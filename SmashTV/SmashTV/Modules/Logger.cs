using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace MesserSmash.Modules {
    class Logger {
        private static string LOG_FILE = "SmashLogFile.txt";
        private static List<string> _logMessages = new List<string>();
         private static StringBuilder sb = new StringBuilder();

        internal static void info(string s, params Object[] p) {
            sb.Clear();
            sb.AppendFormat(s, p);
            _logMessages.Add("INFO: " + sb.ToString());
        }

        public static void flush() {
            //foreach (var i in _logMessages) {
            //    Debug.WriteLine(i);
            //}

            using (StreamWriter sw = new StreamWriter(LOG_FILE, true)) {
                foreach(var line in _logMessages) {
                    sw.WriteLine(line);
                }
            }
            sb.Clear();
            _logMessages.Clear();
        }

        public static void error(string s, params Object[] p) {
            sb.Clear();
            sb.AppendFormat(s, p);
            _logMessages.Add("ERROR: " + sb.ToString());
        }
    }
}
