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

        public SpawnCriteria Criteria;
        private bool _active;

        /// <summary>
        /// Constructor for JSON when creating object
        /// </summary>
        public WaveSpawner()
        {
            _active = true;
        }

        public WaveSpawner(int enemyType, int enemiesToSpawn) {
            Criteria = new SpawnCriteria();
            EnemyType = enemyType;
            SpawnCount = enemiesToSpawn;
            _active = true;
        }

        public void update(GameState state) {
            if (_active == false) {
                return;
            }

            if (Criteria.isFulfilled(state)) {
                new SpawnWaveCommand(this).execute();
            }
        }

        internal void deactivate() {
            _active = false;
        }
    }
}
