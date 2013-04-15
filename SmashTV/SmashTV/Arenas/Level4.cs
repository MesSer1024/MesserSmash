using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    class Level4 : Arena {
         private float _timestampLastSpawnedWave;
        private List<WaveSpawner> _spawners = new List<WaveSpawner>();

        public Level4() {
            _secondsLeft = 60;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 60; i++) {
                WaveSpawner wave;
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryRange, 2 + (int)(i * 0.09f));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 5 + (int)(i * 0.09f));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.37f);
                criteria.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 18;
            _spawners[1].SpawnCount = 18;
            _spawners[4].SpawnCount = 12;
            _spawners[5].SpawnCount = 10;
            var end = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryRange, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
            _spawners.RemoveAt(15);
            _spawners.RemoveAt(15);
            _spawners.RemoveAt(15);
            var foo = new WaveSpawner((int)EnemyTypes.Types.SecondaryRange, 9);
            var crit = new SpawnCriteria();
            crit.MaxEnemiesAlive = 5;
            crit.MinSecondsInArena = (int)(16 * 1.37f);
            foo.addCriteria(crit);
            _spawners.Insert(15, foo);
            _spawners.RemoveAt(7);
            _spawners.RemoveAt(7);
            _spawners.RemoveAt(26);
            _spawners.RemoveAt(26);
            _spawners.RemoveAt(34);
            _spawners.RemoveAt(35);
        }

        public override void startLevel() {
            _timestampLastSpawnedWave = 0;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 4");
        }

        protected override List<Spawnpoint> createSpawnpoints() {
            return Utils.generateSpawnpoints(Bounds);
        }

        protected override void custUpdate(GameState state) {
            _timestampLastSpawnedWave += state.DeltaTime;
            foreach (var wave in _spawners) {
                wave.update(state);
            }
        }

        protected override void custSpawnWaveCommand(WaveSpawner spawner) {
            EnemyTypes.Types enemyType = (EnemyTypes.Types)spawner.EnemyType;
            spawner.deactivate();
            for (int i = 0; i < spawner.SpawnCount; i++) {
                if (enemyType == EnemyTypes.Types.Melee) {
                    getRandomSpawnpoint().generateMeleeEnemies(1);
                } else if (enemyType == EnemyTypes.Types.SecondaryRange) {
                    getRandomSpawnpoint().generateSecondaryRangedEnemies(1);
                }
                
            }
        }
    }
}
