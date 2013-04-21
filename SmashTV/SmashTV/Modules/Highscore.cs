using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MesserSmash.Modules {
    class Highscore {
        private static Highscore _instance;
        public static Highscore Instance { get {
            if (_instance == null) {
                _instance = new Highscore();
            }
            return _instance;
        } }
        private Highscore() {

        }
        private List<string> _players = new List<string>();

        public List<string> Players {
            get { return _players; }
        }
        private List<int> _score = new List<int>();

        public List<int> Score {
            get { return _score; }
        }
        private List<int> _kills = new List<int>();

        public List<int> Kills {
            get { return _kills; }
        }

        public void insertTotalHighscore(string name, int score, int kills) {
            var url = "./highscore.txt";
            load(url);
            int i = 0;

            foreach (var s in _score) {
                if (score < s) {
                    i++;
                } else {
                    break;
                }
            }
            _players.Insert(i, name);
            _score.Insert(i, score);
            _kills.Insert(i, kills);
            overwriteFile(url);
        }

        public void insertLevelHighscore(string name, int level, int score, int kills) {
            var url = "./level" + level + ".txt";
            load(url);
            int i = 0;

            foreach (var s in _score) {
                if (score < s) {
                    i++;
                } else {
                    break;
                }
            }
            _players.Insert(i, name);
            _score.Insert(i, score);
            _kills.Insert(i, kills);
            overwriteFile(url);
        }

        private void overwriteFile(string url) {
            using (StreamWriter sw = new StreamWriter(url, false)) {
                for (int i = 0; i < _score.Count; ++i) {
                    sw.WriteLine(_players[i] + "|" + _score[i] + "|" + _kills[i]);
                }
            }
        }

        public void load(string url) {
            _players.Clear();
            _score.Clear();
            _kills.Clear();

            if (new FileInfo(url).Exists) {
                using (StreamReader sr = new StreamReader(url)) {
                    while (!sr.EndOfStream) {
                        var line = sr.ReadLine().Split('|');
                        _players.Add(line[0]);
                        _score.Add(Int32.Parse(line[1]));
                        _kills.Add(line.Length == 3 && line[2] != "" ? Int32.Parse(line[2]) : 0);
                    }
                }
            }
        }
    }
}
