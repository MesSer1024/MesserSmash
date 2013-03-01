using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash {
    class GameEvent {
        public enum GameEvents{
            PlayerDamaged,
            EnemyKilled,
            GameStarted,
            LevelFinished,
            PlayerDead,
        }
    }
}
