using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
using System.Collections.Generic;
namespace MesserSmash.Arenas {
    public class Level06 : Arena {

        public Level06() {
            _secondsLeft = 60;
            Level = 6;

            WaveSpawner wave;
            WaveSpawner wave2;
            for (int i = 0; i < 40; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.57f);
                criteria.MaxEnemiesAlive = 35;
                wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(5, 3 + (int)(i * 0.09f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 2 + Math.Min(2, 2 + (int)(i * 0.09f)));
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 11;
            _spawners[1].SpawnCount = 11;

            for (int i = 0; i < 6; ++i) {
                var every10Second = new WaveSpawner(0, 21);
                every10Second.CustomSpawnCommand = onCustomSpawn;
                every10Second.addCriteria(new SpawnCriteria() { MinSecondsInArena = 10 * i });
                _spawners.Add(every10Second);
            }

            var middle = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 33 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 5);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 5);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }

        private void onCustomSpawn(WaveSpawner spawner, List<Spawnpoint> spawnpoints) {
            spawner.CustomSpawnCommand = null;
            var sp = spawnpoints[Utils.randomInt(spawnpoints.Count)];
            sp.generateSecondaryMeleeUnits(spawner.SpawnCount);
        } 
    }
}
