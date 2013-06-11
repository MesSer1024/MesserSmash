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
    public class HighscoreContainer {
        private List<Highscore> _highscores;

        public HighscoreContainer() {
            _highscores = new List<Highscore>();
        }

        public void clearData() {
            _highscores.Clear();            
        }

        /// <summary>
        /// Adds highscore to the highscore list if it is valid (has a gameid and sessionid)
        /// </summary>
        /// <param name="score"></param>
        public void addHighscore(Highscore score) {
            if (validHighscore(score) && !_highscores.Exists(a => a.GameId == score.GameId)) {
                _highscores.Add(score);
            }
        }

        private static bool validHighscore(Highscore score) {
            return score.GameId != "" && score.SessionId != "";
        }

        public List<Highscore> getHighscoresOnLevel(uint level) {
            return _highscores.FindAll(a => a.Level == level);
        }


        /// <summary>
        /// Retrieves the locally cached highscores on a specific round
        /// </summary>
        /// <param name="roundId">every level belongs to a set, every set has a round id, one roundId represents for instance level 1-10</param>
        /// <returns>A key-value-set consisting of sessionid & a list of highscores for that sessionid</returns>
        public Dictionary<string, List<Highscore>> getHighscoresOnRound(uint roundId) {
            var items = new Dictionary<string, List<Highscore>>();
            foreach (var i in _highscores.FindAll(a => a.RoundId == roundId)) {
                if (i.SessionId != "" && !items.ContainsKey(i.SessionId)) {
                    items.Add(i.SessionId, _highscores.FindAll(a => a.SessionId == i.SessionId));
                }
            }
            return items;
        }

        public List<Highscore> getMergedHighscoresOnRound(uint roundid) {
            var guiHighscoreList = new List<Highscore>();
            var allItems = getHighscoresOnRound(roundid);
            foreach (var i in allItems) {
                var guiScore = new Highscore();
                var innerList = i.Value;
                guiScore.IsMerged = true;
                guiScore.SessionId = innerList[0].SessionId;
                guiScore.UserId = innerList[0].UserId;
                guiScore.UserName = innerList[0].UserName;

                foreach (var j in innerList) {
                    guiScore.Score += j.Score;
                    guiScore.Kills += j.Kills;
                }
                guiHighscoreList.Add(guiScore);
            }
            return guiHighscoreList;
        }

        public void populateHighscoresFromFile(string highscoreFilename) {
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

        public void writeHighscoresToFile(string relativeFilePath, bool append=false) {
            using (StreamWriter sw = new StreamWriter(relativeFilePath, append)) {
                foreach (var i in _highscores) {
                    sw.WriteLine(i.ToString());
                }
                sw.Flush();
            }
        }

        public void addHighscores(List<Highscore> localScores) {
            foreach (var i in localScores) {
                addHighscore(i);
            }
        }
    }
}
