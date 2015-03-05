using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Modules {
    public class SpawnCriteria {
        public int MaxEnemiesAlive { get; set; }
        public int MinTotalEnemiesKilled { get; set; }
        public int MinSecondsInArena { get; set; }

        public bool Satisfied { get; private set; }

        public SpawnCriteria() {
            MaxEnemiesAlive = -1;
            MinSecondsInArena = -1;
            MinTotalEnemiesKilled = -1;

            Satisfied = false;
        }

        public bool isFulfilled(GameState state) {
            if (Satisfied && (MaxEnemiesAlive < 0 || state.EnemiesAlive <= MaxEnemiesAlive)) {
                return true;
            } else {
                Satisfied = false;
            }

            int numCriteriasMet = 0;
            
            if (MaxEnemiesAlive < 0 || state.EnemiesAlive <= MaxEnemiesAlive) {
                numCriteriasMet++;
            }
            if (MinTotalEnemiesKilled < 0 || state.EnemiesKilled >= MinTotalEnemiesKilled) {
                numCriteriasMet++;
            }
            if (MinSecondsInArena < 0 || state.TimeInArena >= MinSecondsInArena) {
                numCriteriasMet++;
            }

            Satisfied = numCriteriasMet >= 3;
            return Satisfied;
        }

        public void reset()
        {
            Satisfied = false;
        }
    }
}
