using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace LissovLog
{
    public partial class LogControl : ListView
    {
        private LogLevel _currentLevel = LogLevel.DEBUG;
        private ToolStripMenuItem levels;

        public LogControl()
        {
            InitializeComponent();

            this.View = View.Details;
            this.Columns.Add(new ColumnHeader() { Text = "Level" });
            this.Columns.Add(new ColumnHeader() { Text = "Source", Width = 70 });
            this.Columns.Add(new ColumnHeader() { Text = "Time" });
            this.Columns.Add(new ColumnHeader() { Text = "Message", Width = 400 });
            this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(LogControl_RetrieveVirtualItem);
            this.VirtualMode = true;

            #region Menu
            ToolStripMenuItem clearItem = new ToolStripMenuItem("Clear");
            clearItem.Click += new EventHandler(clearItem_Click);
            this.ContextMenuStrip = new ContextMenuStrip();
            this.ContextMenuStrip.Items.Add(clearItem);

            levels = new ToolStripMenuItem("Level");
            LogLevel[] loglevels = new LogLevel[] { LogLevel.DEBUG, LogLevel.WARN, LogLevel.INFO, LogLevel.ERROR };
            foreach (var item in loglevels)
            {
                if (item < Log.MEMORYLEVEL) continue;
                ToolStripMenuItem menu = new ToolStripMenuItem(item.ToString());
                menu.Tag = item;
                menu.Checked = item == _currentLevel;
                menu.Click += new EventHandler(menu_Click);
                levels.DropDown.Items.Add(menu);
            }
            this.ContextMenuStrip.Items.Add(levels);
            #endregion

            ThreadPool.QueueUserWorkItem(UpdateLogCycle);
        }

        void menu_Click(object sender, EventArgs e)
        {
            _currentLevel = (LogLevel)(sender as ToolStripMenuItem).Tag;
            foreach (var item in levels.DropDown.Items)
                (item as ToolStripMenuItem).Checked = (LogLevel)(item as ToolStripMenuItem).Tag == _currentLevel;
            UpdateLog();
        }

        void clearItem_Click(object sender, EventArgs e)
        {
            Log.ClearMemory();
            UpdateLog();
        }

        private void UpdateLogCycle(object o)
        {
            while (true)
            {
                try
                {
                    UpdateLog();
                    Thread.Sleep(1000);
                }
                catch (Exception) { }
            }
        }

        public void UpdateLog()
        {
            if (this.InvokeRequired)
                this.Invoke(new MethodInvoker(delegate() { UpdateLog(); }));
            else
            {
                try
                {
                    this.VirtualListSize = Log.getLog(_currentLevel).Count;
                }
                catch (Exception) { }
            }
        }

        private void LogControl_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex > Log.getLog(_currentLevel).Count)
                    e.Item = new ListViewItem(new string[] {"error", "LissovLog", "error", "error"});
                else
                {
                    e.Item = getItem(Log.getLog(_currentLevel)[e.ItemIndex]);

                }
            }
            catch (Exception)
            {
                e.Item = new ListViewItem();
            }
        }

        private ListViewItem getItem(LogEntry entry)
        {
            ListViewItem res = new ListViewItem(new string[] {
                entry.Level.ToString(), entry.Source, entry.Time.ToShortTimeString(), entry.Message });
            switch (entry.Level)
            {
                case LogLevel.DEBUG:
                    res.ForeColor = Color.Gray;
                    break;
                case LogLevel.WARN:
                    res.ForeColor = Color.Orange;
                    break;
                case LogLevel.ERROR:
                    res.ForeColor = Color.Red;
                    break;
                case LogLevel.INFO:
                default:
                    res.ForeColor = Color.Black;
                    break;
            }
            return res;
        }
    }
}
