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
        private StreamWriter _output;

        public HighscoreContainer() {
            _highscores = new List<Highscore>();
        }

        public void init(string highscoreFilename = "./highscores/highscores.txt") {
            var input = File.ReadAllLines(highscoreFilename);
            for (int i = 0; i < input.Length; ++i)
            {
                var score = Highscore.FromString(input[i]);
                if (score != null)
                    _highscores.Add(score);
                else
                    Logger.error("[init]Could not add highscore (unparsable), data={0}", input[i]);
            }

            _output = new StreamWriter(highscoreFilename, true);
        }

        public void clearData() {
            _highscores.Clear();            
        }

        /// <summary>
        /// Adds highscore to the highscore list if it is valid (has a gameid and sessionid)
        /// </summary>
        /// <param name="score"></param>
        public void validateAndAdd(Highscore score) {
            var valid = score.GameId != "" && score.SessionId != "";
            var unique = !_highscores.Exists(a => a.GameId == score.GameId);
            if (valid && unique)
            {
                _highscores.Add(score);
                //used on both client & server...
                if(_output != null)
                    _output.WriteLine(score.ToString());
            }
            else
                Logger.error("Could not add highscore: valid:{1}, unique:{2}, data={0}", score.ToString(), valid, unique);
        }

        public List<Highscore> getHighscoresOnLevel(uint level) {
            return _highscores.FindAll(a => a.Level == level).OrderByDescending(a => a.Score).ToList();
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
            return guiHighscoreList.OrderByDescending(a => a.Score).ToList();
        }

        public void addHighscores(List<Highscore> localScores) {
            foreach (var i in localScores) {
                validateAndAdd(i);
            }
        }

        public void close()
        {
            _output.Flush();
            _output.Close();
        }
    }
}
