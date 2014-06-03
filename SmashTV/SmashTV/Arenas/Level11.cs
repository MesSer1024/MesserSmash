using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Commands;
using System.Collections;
using MesserSmash.Enemies;

namespace MesserSmash.Arenas {
    public class Level11 : Arena {

        public Level11() {
            Level = 11;
            _secondsLeft = 60;

            WaveSpawner wave, wave2;
            for (int i = 0; i < 40; i++) {
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.17f);
                criteria.MaxEnemiesAlive = 30;
                wave = new WaveSpawner((int)EnemyTypes.Types.Range, Math.Min(5, 2 + (int)(i * 0.09f)));
                wave2 = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, Math.Min(9, 5 + (int)(i * 0.09f)));
                wave.addCriteria(criteria);
                wave2.addCriteria(criteria);
                _spawners.Add(wave);
                _spawners.Add(wave2);
            }

            _spawners[0].SpawnCount = 9;
            _spawners[1].SpawnCount = 9;
            _spawners[4].SpawnCount = 6;
            _spawners[5].SpawnCount = 5;
            _spawners[8].SpawnCount = 12;
            _spawners[9].SpawnCount = 13;

            var middle = new WaveSpawner((int)EnemyTypes.Types.Melee, 17);
            middle.addCriteria(new SpawnCriteria { MinSecondsInArena = 32 });
            _spawners.Add(middle);
            var middle2 = new WaveSpawner((int)EnemyTypes.Types.Range, 6);
            middle2.addCriteria(new SpawnCriteria { MinSecondsInArena = 43 });
            _spawners.Add(middle2);

            var end = new WaveSpawner((int)EnemyTypes.Types.SecondaryMelee, 8);
            end.addCriteria(new SpawnCriteria { MinSecondsInArena = 55, MaxEnemiesAlive = 35 });
            var end2 = new WaveSpawner((int)EnemyTypes.Types.Range, 8);
            end2.addCriteria(new SpawnCriteria { MinSecondsInArena = 54, MaxEnemiesAlive = 35 });

            _spawners.Add(end);
            _spawners.Add(end2);
        }
    }
}
