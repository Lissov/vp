using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace LissovLog
{
    public enum LogLevel { DEBUG = 0, INFO = 10, WARN = 20, ERROR = 30};
    public class LogEntry
    {
        public LogLevel Level;
        public DateTime Time;
        public string Message;
        public string Source;
    }
    public static class Log
    {
        public static bool TestMode = false;

        public const LogLevel FILEWRITELEVEL = LogLevel.DEBUG;
        public const LogLevel MEMORYLEVEL = LogLevel.INFO;

        private static StreamWriter writer;
        public static List<LogEntry> memoryLog;

        public static event EventHandler MemoryLogChanged;

        public static void Prepare(string filename)
        {
            if (!TestMode)
            {
                try
                {
                    string path = Path.GetDirectoryName(Application.ExecutablePath);
                    writer = new StreamWriter(path + "\\" + filename);
                    writer.WriteLine("Model Log");
                    writer.WriteLine("by Pavlo Lissov");
                    writer.WriteLine(DateTime.Now.ToString());
                    writer.WriteLine("=======================");
                    writer.Flush();
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }

            try
            {
                memoryLog = new List<LogEntry>();
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public static void Write(LogLevel level, bool roundDoubles, string format, params object[] pars)
        {
            if (roundDoubles)
            {
                for (int i = 0; i < pars.Length; i++)
                {
                    if (pars[i] is double)
                        pars[i] = Math.Round((double)pars[i], 6);
                }
            }
            Write(level, format, pars);
        }

        public static void Write(LogLevel level, string format, params object[] pars)
        {
            Write(Assembly.GetCallingAssembly().GetName().Name, level, format, pars);
        }

        public static void Write(string source, LogLevel level, string format, params object[] pars)
        {
            Write(source, level, string.Format(format, pars));
        }
        
        public static void Write(LogLevel level, string message)
        {
            Write(Assembly.GetCallingAssembly().GetName().Name, level, message);
        }

        public static void Write(string source, LogLevel level, string message)
        {
            if (!TestMode)
            {
                try
                {
                    string logentry = string.Format("{0}, {1} : {2} -> {3}", level.ToString(), source, DateTime.Now.ToShortTimeString(), message);
                    if (level >= FILEWRITELEVEL)
                    {
                        writer.WriteLine(logentry);
                        writer.Flush();
                    }
                }
                catch (Exception ex)
                {
                    Debugger.Break();
                }
            }

            try
            {
                if (level >= MEMORYLEVEL)
                {
                    memoryLog.Add(new LogEntry() { Level = level, Time = DateTime.Now, Message = message, Source = source });
                    EventHandler e = MemoryLogChanged;
                    if (e != null)
                        e(null, EventArgs.Empty);
                }
            }
            catch (Exception ex)
            {
                Debugger.Break();
            }
        }

        public static void Close()
        {
            writer.Flush();
            writer.WriteLine("=======================");
            writer.WriteLine(DateTime.Now.ToString() + "   log closed.");
            writer.Close();
        }

        public static void ClearMemory()
        {
            memoryLog = new List<LogEntry>();
            EventHandler e = MemoryLogChanged;
            if (e != null)
                e(null, EventArgs.Empty);
        }

        internal static List<LogEntry> getLog(LogLevel level)
        {
            if (level <= MEMORYLEVEL) return memoryLog;
            var entries = from m in memoryLog
                               where m.Level >= level
                               select m;
            return new List<LogEntry>(entries);
        }

        public static void Assert(bool condition, LogLevel level, string message)
        {
            if (!condition) Write(level, message);
        }
    }
}
