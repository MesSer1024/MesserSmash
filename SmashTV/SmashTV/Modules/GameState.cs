using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Modules {
    public class GameState {
        public GameState(float deltatime, float timeInArena, int enemiesAlive, int enemiesKilled) {
            TimeInArena = timeInArena;
            EnemiesAlive = enemiesAlive;
            EnemiesKilled = enemiesKilled;
            DeltaTime = deltatime;
        }

        public float TimeInArena { get; private set; }
        public int EnemiesAlive { get; private set; }
        public int EnemiesKilled { get; private set; }
        public float DeltaTime { get; private set; }
    }
}
