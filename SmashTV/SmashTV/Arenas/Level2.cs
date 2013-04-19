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

        public Level2() {
            _secondsLeft = 60;

            for (int i = 0; i < 30; i++) {
                var wave = new WaveSpawner((int)EnemyTypes.Types.Range, 4 + Math.Min(8, (int)(i * 0.29f)));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 2.40f);
                criteria.MaxEnemiesAlive = 35;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 16;
            _spawners[1].SpawnCount = 12;
            _spawners[4].SpawnCount = 14;
            _spawners[7].SpawnCount = 10;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Range, 11);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 27 });
            _spawners.Add(middle);

            var end = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 40, MaxEnemiesAlive = 45 });
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Range, 2);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 60 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.Range, 9);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 55, MaxEnemiesAlive = 60 });

            _spawners.Add(end);
            _spawners.Add(end3);
            _spawners.Add(end4);
        }
    }
}
