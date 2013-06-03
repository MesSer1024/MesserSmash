using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level04 : Arena {

        public Level04() {
            _secondsLeft = 60;
            Level = 4;

            for (int i = 0; i < 30; i++) {
                var wave = new WaveSpawner((int)EnemyTypes.Types.Range, 3 + Math.Min(2, (int)(i * 0.29f)));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.73f);
                criteria.MaxEnemiesAlive = 15;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 15;
            _spawners[1].SpawnCount = 6;
            _spawners[4].SpawnCount = 9;
            _spawners[7].SpawnCount = 6;

            var end3 = new WaveSpawner((int)EnemyTypes.Types.Range, 8);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 18 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.Range, 9);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 53, MaxEnemiesAlive = 30 });

            _spawners.Add(end3);
            _spawners.Add(end4);
        }
    }
}
