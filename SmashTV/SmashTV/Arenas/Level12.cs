using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Commands;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    class Level12 : Arena {

        public Level12() {
            _secondsLeft = 60;
            Level = 12;

            WaveSpawner wave1, wave2, wave3;
            for (int i = 0; i < 60; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.09f);
                criteria.MaxEnemiesAlive = 30;
                wave1 = new WaveSpawner((int)EnemyTypes.Types.Melee, Math.Min(8, 2 + (int)(i * 0.49f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(3, 2 + (int)(i * 0.09f)));
                wave3 = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(3, 2 + (int)(i * 0.09f)));
                wave1.addCriteria(criteria);
                wave2.addCriteria(criteria);
                wave3.addCriteria(criteria);
                _spawners.Add(wave1);
                _spawners.Add(wave2);
                _spawners.Add(wave3);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[2].SpawnCount = 9;
            _spawners[9].SpawnCount = 4;
            _spawners[10].SpawnCount = 8;
            _spawners[11].SpawnCount = 3;

            //var middle = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 7);
            //middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 37 });
            //_spawners.Add(middle);
            //var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            //middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 36 });
            //_spawners.Add(middle2);

            //var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 4);
            //end.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });
            //var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 4);
            //end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 46, MaxEnemiesAlive = 35 });

            //_spawners.Add(end);
            //_spawners.Add(end2);
        }
    }
}
