using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas
{
    class Level6 : Arena
    {
        public Level6()
        {
            _secondsLeft = 60;
            Level = 6;

            WaveSpawner wave;
            for (int i = 0; i < 30; i++)
            {
                if (i % 2 == 0)
                {
                    wave = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4 + (int)(i * 0.29f));
                }
                else
                {
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
            _spawners[4].SpawnCount = 16;
            _spawners[5].SpawnCount = 16;
            var end = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 43, MaxEnemiesAlive = 35 });
            var end3 = new WaveSpawner((int)EnemyTypes.Types.Melee, 6);
            end3.addCriteria(new SpawnCriteria { MinSecondsInArena = 51, MaxEnemiesAlive = 42 });
            var end4 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            end4.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 45 });
            var end5 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 8);
            end5.addCriteria(new SpawnCriteria { MinSecondsInArena = 57, MaxEnemiesAlive = 45 });

            _spawners.Add(end);
            _spawners.Add(end2);
            _spawners.Add(end3);
            _spawners.Add(end4);
            _spawners.Add(end5);
        }
    }
}
