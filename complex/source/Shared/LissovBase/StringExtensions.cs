using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LissovBase
{
    public static class StringExtensions
    {
        public static string ToIdentifier(this string name)
        {
            return name.ToIdentifier(false);
        }

        public static string ToIdentifier(this string name, bool noOfIn)
        {
            if (string.IsNullOrEmpty(name)) return string.Empty;
            string[] parts = name.Split(" -()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            string res = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                if (noOfIn && (parts[i].ToLower().Equals("of") 
                            || parts[i].ToLower().Equals("in")
                            || parts[i].ToLower().Equals("for")
                            || parts[i].ToLower().Equals("to")
                            || parts[i].ToLower().Equals("from")))
                    continue;

                res += char.ToUpper(parts[i][0]) + parts[i].Substring(1);
            }
            return res;
        }

    }
}
