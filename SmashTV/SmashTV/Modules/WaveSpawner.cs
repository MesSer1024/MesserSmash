using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash.Modules {
    public class WaveSpawner {
        public int EnemyType { get; private set; }
        public int SpawnCount { get; private set; }

        private List<SpawnCriteria> _criterias;
        private bool _active;

        public WaveSpawner(int enemyType, int enemiesToSpawn) {
            _criterias = new List<SpawnCriteria>();
            EnemyType = enemyType;
            SpawnCount = enemiesToSpawn;
            _active = true;
        }

        public void addCriteria(SpawnCriteria criteria) {
            _criterias.Add(criteria);
        }

        public void update(GameState state) {
            if (_active == false) {
                return;
            }
            int fulfilledCriterias = 0;
            foreach (var criteria in _criterias) {
                if (criteria.isFulfilled(state)) {
                    fulfilledCriterias++;
                }
            }

            if (fulfilledCriterias == _criterias.Count) {
                new SpawnWaveCommand(this).execute();
            }
        }

        internal void deactivate() {
            _active = false;
        }
    }
}
