using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using SharedSmashResources;
using MesserSmash.Modules;

namespace SharedSmashResources {
    public class HighscoreCollection : Dictionary<uint, List<Highscore>> { };

    public class HighscoreContainer {
        private HighscoreCollection _highscores;

        public HighscoreContainer() {
            _highscores = new HighscoreCollection();
        }

        public void addHighscore(Highscore score) {
            try {
                if (!_highscores.ContainsKey(score.Level)) {
                    _highscores.Add(score.Level, new List<Highscore>());
                }
                _highscores[score.Level].Add(score);
            } catch (Exception e) {
                Logger.error("Timing Issues in addHighscore score={0} exception={1}", score, e.ToString());
            }
        }

        public List<Highscore> getHighscoresOnLevel(uint level) {
            if (_highscores.ContainsKey(level)) {
                return _highscores[level];
            }
            return new List<Highscore>();
        }

        public void populateHighscores(string highscoreFilename) {
            var fi = new FileInfo(highscoreFilename);
            if (fi.Exists) {
                using (StreamReader sr = new StreamReader(highscoreFilename)) {
                    while (!sr.EndOfStream) {
                        var score = Highscore.FromString(sr.ReadLine());
                        addHighscore(score);
                    }
                }
            }
        }

        public void outputToFile(string relativeFilePath, bool append=false) {
            using (StreamWriter sw = new StreamWriter(relativeFilePath, append)) {
                foreach (var level in _highscores.Values) {
                    foreach (var score in level) {
                        sw.WriteLine(score.ToString());
                    }
                }
                sw.Flush();
            }
        }

        public void clearData() {
            foreach (var level in _highscores.Values) {
                level.Clear();
            }
        }

        public void clearData(uint level) {
            if (_highscores.ContainsKey(level)) {
                _highscores[level].Clear();
            }
        }
    }
}
