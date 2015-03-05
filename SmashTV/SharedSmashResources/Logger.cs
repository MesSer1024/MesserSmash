using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace MesserSmash.Modules {
    public static class Logger {
        [Flags]
        private enum State : uint {
            None = 1 << 0,
            StreamOpen = 1 << 1,
        }

        public enum LogLevels
        {
            i0_Spam,
            i1_Debug,
            i2_Info,
            i3_Error,
        }
        public static LogLevels LogLevel = LogLevels.i1_Debug;

        private const string DEFAULT_LOG_FILE = "SmashLogFile.txt";
        private static List<string> _logMessages;
        private static StringBuilder sb;
        private static StreamWriter sw;
        private static object ThreadLock = new object();
        private static State _flag;

        public static void info(string s) {
            info("{0}", s);
        }

        public static void info(string s, params Object[] p)
        {
            if (LogLevel > LogLevels.i2_Info)
                return;
            lock (ThreadLock) {
                if ((_flag & State.StreamOpen) != State.StreamOpen)
                    return;
                sb.Clear();
                sb.AppendFormat("[info|{0}] ", DateTime.Now.Ticks);
                sb.AppendFormat(s, p);
                _logMessages.Add(sb.ToString());
                sw.WriteLine(sb.ToString());
            }
        }

        public static void error(string s, params Object[] p) {
            lock (ThreadLock) {
                if ((_flag & State.StreamOpen) != State.StreamOpen)
                    return;
                sb.Clear();
                sb.AppendFormat("[error|{0}] ", DateTime.Now.Ticks);
                sb.AppendFormat(s, p);
                _logMessages.Add(sb.ToString());
                sw.WriteLine(sb.ToString());
            }
        }

        public static void flushToFile(string url) {
            lock (ThreadLock) {
                if ((_flag & State.StreamOpen) != State.StreamOpen)
                    return;
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
        }

        public static void init(string fileUrl = DEFAULT_LOG_FILE) {
            clean();
            _logMessages = new List<string>();
            sb = new StringBuilder();
            sw = new StreamWriter(fileUrl, true);
            _flag |= State.StreamOpen;
        }

        public static void clean() {
            lock (ThreadLock) {
                _flag &= ~State.StreamOpen; //AND on an Inverted StreamOpen (clears only that flag)
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

        public static void debug(string s) {
            debug("{0}", s);
        }

        public static void debug(string s, params Object[] p) {
            if (LogLevel > LogLevels.i1_Debug)
                return;
            lock (ThreadLock) {
                if ((_flag & State.StreamOpen) != State.StreamOpen)
                    return;
                sb.Clear();
                sb.AppendFormat("[debug|{0}] ", DateTime.Now.Ticks);
                sb.AppendFormat(s, p);
                _logMessages.Add(sb.ToString());
                sw.WriteLine(sb.ToString());
            }
        }

        public static void spam(string s, params Object[] p)
        {
            if (LogLevel > LogLevels.i0_Spam)
                return;
            lock (ThreadLock)
            {
                if ((_flag & State.StreamOpen) != State.StreamOpen)
                    return;
                sb.Clear();
                sb.AppendFormat("[spam|{0}] ", DateTime.Now.Ticks);
                sb.AppendFormat(s, p);
                _logMessages.Add(sb.ToString());
                sw.WriteLine(sb.ToString());
            }
        }
    }
}
