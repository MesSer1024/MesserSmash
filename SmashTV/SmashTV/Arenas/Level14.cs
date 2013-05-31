using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    class Level14 : Arena {

        public Level14() {
            _secondsLeft = 60;
            Level = 14;
            WaveSpawner wave;
            for (int i = 0; i < 60; i++) {
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range,Math.Min(5, 2 + (int)(i * 0.09f)));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(9, 5 + (int)(i * 0.09f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.67f);
                criteria.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            for (int i = 0; i < 20; i++) {
                wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
                wave.addCriteria(new SpawnCriteria { MinSecondsInArena = i * 6 });
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 18;
            _spawners[1].SpawnCount = 18;
            _spawners[4].SpawnCount = 12;
            _spawners[5].SpawnCount = 10;

            var middle = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 11);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 37 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
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
