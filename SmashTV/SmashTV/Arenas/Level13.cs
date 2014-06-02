using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;
using MesserSmash.Commands;

namespace MesserSmash.Arenas {
    class Level13 : Arena {

        public Level13() {
            _secondsLeft = 60;
            Level = 13;

            WaveSpawner wave;
            for (int i = 0; i < 60; i++) {
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(6, 2 + (int)(i * 0.39f)));
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(4, 2 + (int)(i * 0.39f)));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.77f);
                criteria.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            for (int i = 0; i < 5; ++i) {
                wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 9);
                var wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 9);
                var criteria = new SpawnCriteria();
                var criteria2 = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 10.23f + 9.23f);
                criteria2.MinSecondsInArena = (int)(i * 10.23f + 10.23f);
                criteria.MaxEnemiesAlive = criteria2.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria2);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 6;
            _spawners[5].SpawnCount = 5;

            var middle = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 7);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 37 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}
