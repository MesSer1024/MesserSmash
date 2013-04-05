using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Modules {
    public class GameState {
        public GameState(float deltatime, float timeInArena) {
            DeltaTime = deltatime;
            TimeInArena = timeInArena;
        }

        public float DeltaTime { get; private set; }
        public float TimeInArena { get; private set; }

        public int EnemiesAlive { get { return (int)DataDefines.ID_STATE_ENEMIES_ALIVE; } }
        public int EnemiesKilled { get { return (int)DataDefines.ID_STATE_ENEMIES_KILLED; } }
    }
}
