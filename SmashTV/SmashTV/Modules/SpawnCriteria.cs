using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Modules {
    public class SpawnCriteria {
        public int MaxEnemiesAlive { get; set; }
        public int MinTotalEnemiesKilled { get; set; }
        public int MinSecondsInArena { get; set; }
        public int WaveRepeatableCount { get; set; }
        public int SecondsBetweenRepeat { get; set; }

        public bool Active { get; private set; }
        public bool Satisfied { get; private set; }
        
        private int _repeatCounter;
        private float _timeSinceRepeat;

        public SpawnCriteria(int repeatCount = 0, int secondsBetweenRepeat = 1) {
            MaxEnemiesAlive = -1;
            MinSecondsInArena = -1;
            MinTotalEnemiesKilled = -1;
            WaveRepeatableCount = repeatCount;
            SecondsBetweenRepeat = secondsBetweenRepeat;

            _repeatCounter = 0;
            _timeSinceRepeat = 10000000;

            Satisfied = false;
            Active = true;
        }

        public bool isFulfilled(GameState state) {
            if (Active == false) {
                return true;
            }

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
    }
}
