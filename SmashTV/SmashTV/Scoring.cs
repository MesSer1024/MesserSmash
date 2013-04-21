using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash {
    static class Scoring {
        private static List<LevelScore> _levelScores = new List<LevelScore>();
        private static LevelScore _currLevel;

        public static void reset() {
            _levelScores.Clear();
            _currLevel = null;
        }

        public static void setLevel(int level) {
            _currLevel = null;
            foreach (var i in _levelScores) {
                if (i.Level == level) {
                    _currLevel = i;
                }
            }
            if (_currLevel == null) {
                _currLevel = new LevelScore(level);
                _levelScores.Add(_currLevel);
            }
        }

        public static void awardScore(float score) {
            _currLevel.Score += (int)score;
        }

        public static void setKillsOnLevel(int kills) {
            _currLevel.Kills = kills;
        }

        public static void onLoot(Loot loot) {
            var score = loot.Type == Arenas.Arena.LootType.Money ? 200 : 10;
            _currLevel.Score += score;
        }

        public static int getTotalKills() {
            var val = 0;
            foreach (var i in _levelScores) {
                val += i.Kills;
            }
            return val;
        }

        public static float getTotalScore() {
            var val = 0.0f;
            foreach (var i in _levelScores) {
                val += i.Score;
            }
            return val;
        }

        public static List<LevelScore> getLevelScores() {
            return _levelScores;
        }
    }
}
