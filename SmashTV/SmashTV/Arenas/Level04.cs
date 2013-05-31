using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level04 : Arena {

        public Level04() {
            _secondsLeft = 60;
            Level = 4;
            WaveSpawner wave;
            for (int i = 0; i < 60; i++) {
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(4, 2 + (int)(i * 0.09f)));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(7, 5 + (int)(i * 0.09f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.17f);
                criteria.MaxEnemiesAlive = 35;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 7;
            _spawners[5].SpawnCount = 10;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Melee, 8);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 37 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 3);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}
