using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MesserSmash.Modules {
    public static class Logger {

        private const string DEFAULT_LOG_FILE = "SmashLogFile.txt";
        private static List<string> _logMessages;
         private static StringBuilder sb;
         private static StreamWriter sw;

         public static void info(string s, params Object[] p) {
             sb.Clear();
             sb.Append("[info] ");
             sb.AppendFormat(s, p);
             _logMessages.Add(sb.ToString());
             sw.WriteLine(sb.ToString());
         }

        public static void error(string s, params Object[] p) {
            sb.Clear();
            sb.Append("[error] ");
            sb.AppendFormat(s, p);
            _logMessages.Add(sb.ToString());
            sw.WriteLine(sb.ToString());
        }

        public static void flushToFile(string url) {
            if (_logMessages.Count > 0) {
                using (StreamWriter sw = new StreamWriter(url, true)) {
                    foreach (var line in _logMessages) {
                        sw.WriteLine(line);
                    }
                }
            }
            sb.Clear();
            _logMessages.Clear();
        }

        public static void init(string fileUrl = DEFAULT_LOG_FILE) {
            clean();
            _logMessages = new List<string>();
            sb = new StringBuilder();
            sw = new StreamWriter(fileUrl, true);            
        }

        public static void clean() {
            if (sw != null) {
                sw.Flush();
                sw.Close();
            }
            if (sb != null) {
                sb.Clear();
            }
            if (_logMessages != null) {
                _logMessages.Clear();
            }
        }
    }
}
