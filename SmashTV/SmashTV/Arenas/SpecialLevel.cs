using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    class SpecialLevel : Arena {
        private int _enemyCounter;

        public SpecialLevel() {
            Level = 100;
        }

        public override void custStartLevel() {
            _secondsLeft = 9000;
            _enemyCounter = 1;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level Endless Stream started");
            createEnemies(48);
        }

        protected override void custUpdate(GameState state) {

            if (canCreateEnemies()) {
                createEnemies((int)(1 + _enemyCounter++ * 0.081f));
            }
        }
    }
}
