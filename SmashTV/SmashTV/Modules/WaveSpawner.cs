using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using System.Collections.ObjectModel;

namespace MesserSmash.Modules {
    public class WaveSpawner {
        public int EnemyType { get; private set; }
        public int SpawnCount { get; private set; }

        public List<SpawnCriteria> Criterias;
        private bool _active;

        public WaveSpawner(int enemyType, int enemiesToSpawn) {
            Criterias = new List<SpawnCriteria>();
            EnemyType = enemyType;
            SpawnCount = enemiesToSpawn;
            _active = true;
        }

        public void addCriteria(SpawnCriteria criteria) {
            Criterias.Add(criteria);
        }

        public void update(GameState state) {
            if (_active == false) {
                return;
            }
            int fulfilledCriterias = 0;
            foreach (var criteria in Criterias) {
                if (criteria.isFulfilled(state)) {
                    fulfilledCriterias++;
                }
            }

            if (fulfilledCriterias == Criterias.Count) {
                new SpawnWaveCommand(this).execute();
            }
        }

        internal void deactivate() {
            _active = false;
        }
    }
}
