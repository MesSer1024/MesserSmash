using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash {
    static class Scoring {
        private static float _score;

        public static void reset() {
            destroy();
            _score = 0;
            Controller.instance.registerInterest(DeadEnemyCommand.NAME, onEnemyDied);
            Controller.instance.registerInterest(LootPickedUpCommand.NAME, onLoot);
        }

        public static float getScore() {
            return _score;
        }

        private static void onEnemyDied(ICommand command) {
            var cmd = command as DeadEnemyCommand;
            _score += cmd.Enemy.Score;
        }

        private static void onLoot(ICommand command) {
            var cmd = command as LootPickedUpCommand;
            var score = cmd.LootReached.Type == Arenas.Arena.LootType.Money ? 200 : 10;
            _score += score;
        }

        public static void destroy() {
            Controller.instance.removeInterest(DeadEnemyCommand.NAME, onEnemyDied);
            Controller.instance.registerInterest(LootPickedUpCommand.NAME, onLoot);
        }
    }
}
