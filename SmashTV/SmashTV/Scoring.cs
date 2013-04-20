using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash {
    static class Scoring {
        private static float _score;
        private static float _kills;

        public static void reset() {
            _score = 0;
            _kills = 0;
        }

        public static float getScore() {
            return _score;
        }

        public static void awardScore(float score) {
            _score += score;
        }

        public static void setKillsOnLevel(float kills) {
            _kills += kills;
        }

        public static void onLoot(Loot loot) {
            var score = loot.Type == Arenas.Arena.LootType.Money ? 200 : 10;
            _score += score;
        }

        public static float getKills() {
            return _kills;
        }
    }
}
