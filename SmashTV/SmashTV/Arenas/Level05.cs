using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level05 : Arena {

        public Level05()
        {
            _secondsLeft = 60;
            Level = 5;

            WaveSpawner wave;
            for (int i = 0; i < 60; i++)
            {
                if (i % 2 == 0)
                {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(6, 4 + (int)(i * 0.09f)));
                }
                else
                {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(3, 2 + (int)(i * 0.09f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.27f);
                criteria.MaxEnemiesAlive = 30;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 5;
            _spawners[5].SpawnCount = 6;

            {
                var middle = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
                middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 23 });
                _spawners.Add(middle);
                var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
                middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 24 });
                _spawners.Add(middle2);
            }

            {
                var middle = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 2);
                middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 33 });
                _spawners.Add(middle);
                var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 3);
                middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 34 });
                _spawners.Add(middle2);
            }

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 7);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 48, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 10);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 49, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}
