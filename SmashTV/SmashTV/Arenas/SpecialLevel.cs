using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    class SpecialLevel : Arena {
        public SpecialLevel() {
        }

        public override void startLevel() {
            _secondsLeft = 9000;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level Endless Stream started");

        }

        protected override void custUpdate(GameState state) {
            if (canCreateEnemies()) {
                createEnemies(10);
            }
        }
    }
}
