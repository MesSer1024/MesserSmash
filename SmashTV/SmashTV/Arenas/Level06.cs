using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level06 : Arena {

        public Level06() {
            _secondsLeft = 60;
            Level = 6;

            WaveSpawner wave;
            WaveSpawner wave2;
            for (int i = 0; i < 40; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.87f);
                criteria.MaxEnemiesAlive = 35;
                wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(5, 2 + (int)(i * 0.09f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(9, 5 + (int)(i * 0.09f)));
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 11;
            _spawners[1].SpawnCount = 11;
            _spawners[4].SpawnCount = 6;
            _spawners[5].SpawnCount = 5;
            _spawners[9].SpawnCount = 11;
            _spawners[10].SpawnCount = 12;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 33 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}
