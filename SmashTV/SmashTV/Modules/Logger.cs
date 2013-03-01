using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MesserSmash.Modules {
    class Logger {
        private static List<string> _logMessages = new List<string>();
         private static StringBuilder sb = new StringBuilder();

        internal static void info(string s, params Object[] p) {
            sb.Clear();
            sb.AppendFormat(s, p);
            _logMessages.Add(sb.ToString());
        }

        public static void flush() {
            foreach (var i in _logMessages) {
                Debug.WriteLine(i);
            }
            sb.Clear();
            _logMessages.Clear();
        }
    }
}
