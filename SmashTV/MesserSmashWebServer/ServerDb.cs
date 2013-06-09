using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MesserSmashWebServer {
    internal class ServerDb {
        private List<GameEntry> _savedStates;            
        private string fileName = "./data/ServerDb.txt";
        private StreamWriter _appendStream = new StreamWriter(String.Format("./data/{0}_ServerDb.txt", DateTime.Now.Ticks), true);

        public void init() {
            _savedStates = new List<GameEntry>();
            var fi = new FileInfo(fileName);
            if (fi.Exists) {
                using (StreamReader sr = new StreamReader(fileName)) {
                    while (!sr.EndOfStream) {
                        var entry = GameEntry.FromString(sr.ReadLine());
                        _savedStates.Add(entry);
                    }
                }
            }
        }

        public void addEntry(GameEntry entry) {
            _savedStates.Add(entry);
            _appendStream.WriteLine(entry.ToString());
        }

        public void close() {
            _appendStream.Close();
            StringBuilder sb = new StringBuilder();
            using (StreamWriter sw = new StreamWriter(fileName, false)) {
                foreach (var item in _savedStates) {
                    sb.AppendLine(item.ToString());
                }
                sw.Write(sb.ToString());
                sw.Flush();
            }
        }
    }
}
