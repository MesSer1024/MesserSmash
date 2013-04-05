using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Commands;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    class Level2 : Arena {
        private float _timestampLastSpawnedWave;
        private List<WaveSpawner> _spawners = new List<WaveSpawner>();

        public Level2() {
            _secondsLeft = 60;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 30; i++) {
                var wave = new WaveSpawner((int)EnemyTypes.Types.Range, 4 + (int)(i * 0.09f));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.40f);
                criteria.MaxEnemiesAlive = 20;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 24;
            _spawners[4].SpawnCount = 14;
            _spawners[7].SpawnCount = 14;
            var end = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 35 });
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 42 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 45 });
            var end5 = new WaveSpawner((int)EnemyTypes.Types.Range, 8);
            end5.addCriteria(new SpawnCriteria { MinSecondsInArena = 57, MaxEnemiesAlive = 45 });

            _spawners.Add(end);
            _spawners.Add(end2);
            _spawners.Add(end3);
            _spawners.Add(end4);
            _spawners.Add(end5);
        }

        protected override List<Spawnpoint> createSpawnpoints() {
            return Utils.generateSpawnpoints(Bounds);
        }

        public override void startLevel() {
            new LevelStartedCommand(this).execute();
            //EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 1");
            _timestampLastSpawnedWave = 0;
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
                getRandomSpawnpoint().generateSecondaryRangedEnemies(1);
            }
        }
    }
}
