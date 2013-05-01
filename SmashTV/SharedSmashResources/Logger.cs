using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MesserSmash.Modules {
    public static class Logger {

        private static string LOG_FILE = "SmashLogFile.txt";
        private static List<string> _logMessages = new List<string>();
         private static StringBuilder sb = new StringBuilder();

         public static void info(string s, params Object[] p) {
            sb.Clear();
            sb.AppendFormat(s, p);
            _logMessages.Add("[info] " + sb.ToString());
        }

        public static void error(string s, params Object[] p) {
            sb.Clear();
            sb.AppendFormat(s, p);
            _logMessages.Add("[error] " + sb.ToString());
        }

        public static void flush() {
            using (StreamWriter sw = new StreamWriter(LOG_FILE, true)) {
                foreach(var line in _logMessages) {
                    sw.WriteLine(line);
                }
            }
            sb.Clear();
            _logMessages.Clear();
        }
    }
}
