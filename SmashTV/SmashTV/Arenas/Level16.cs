using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas
{
    class Level16 : Arena
    {
        public Level16()
        {
            _secondsLeft = 60;
            Level = 16;

            for (int i = 0; i < 30; i++) {
                WaveSpawner wave;
                if (i % 2 == 0) {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 5);
                } else {
                    wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 5 + (int)(i * 0.29f));
                }
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 1.37f);
                criteria.MaxEnemiesAlive = 50;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 18;
            _spawners[1].SpawnCount = 18;
            _spawners[4].SpawnCount = 8;
            _spawners[5].SpawnCount = 16;
            var end10 = new WaveSpawner((int)EnemyTypes.Types.Melee, 22);
            end10.addCriteria(new SpawnCriteria { MinSecondsInArena = 39, MaxEnemiesAlive = 70 });
            var end = new WaveSpawner((int)EnemyTypes.Types.Melee, 23);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 70 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 18);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 41, MaxEnemiesAlive = 70 });
            var end11 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 18);
            end11.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 70 });
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Melee, 10);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 100 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 6);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 100 });
            var end5 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 8);
            end5.addCriteria(new SpawnCriteria { MinSecondsInArena = 53, MaxEnemiesAlive = 100 });

            _spawners.Add(end);
            _spawners.Add(end2);
            _spawners.Add(end3);
            _spawners.Add(end4);
            _spawners.Add(end5);
        }
    }
}
