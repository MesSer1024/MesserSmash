using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level02 : Arena {

        public Level02() {
            _secondsLeft = 60;
            Level = 2;

            for (int i = 0; i < 40; i++) {
                WaveSpawner wave;
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 3);
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 2 + Math.Min(4, (int)(i * 0.29f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.89f);
                criteria.MaxEnemiesAlive = 26;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 8;
            _spawners[1].SpawnCount = 10;
            _spawners[4].SpawnCount = 5;
            _spawners[5].SpawnCount = 5;
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 49, MaxEnemiesAlive = 100 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 3);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 50, MaxEnemiesAlive = 100 });
            var end5 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 5);
            end5.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 100 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Melee, 6);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 53, MaxEnemiesAlive = 100 });

            _spawners.Add(end2);
            _spawners.Add(end3);
            _spawners.Add(end4);
            _spawners.Add(end5);
        }
    }
}
