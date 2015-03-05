using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using System.Collections.ObjectModel;

namespace MesserSmash.Modules {
    public class WaveSpawner {
        public int EnemyType { get; set; }
        public int SpawnCount { get; set; }
        public bool SameSpawnpoint { get; set; }

        public SpawnCriteria Criteria;
        private bool _active;

        /// <summary>
        /// Constructor for JSON when creating object
        /// </summary>
        public WaveSpawner()
        {
            _active = true;
            SameSpawnpoint = false;
        }

        public WaveSpawner(int enemyType, int enemiesToSpawn):base() {
            Criteria = new SpawnCriteria();
            EnemyType = enemyType;
            SpawnCount = enemiesToSpawn;
        }

        public void update(GameState state) {
            if (_active == false) {
                return;
            }

            if (Criteria.isFulfilled(state)) {
                new SpawnWaveCommand(this).execute();
            }
        }

        internal void enabled(bool isEnabled) {
            if (isEnabled)
                Criteria.reset();
            _active = isEnabled;
        }
    }
}
