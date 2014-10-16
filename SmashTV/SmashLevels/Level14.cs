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
            for (int i = 0; i < 20; i++) {
                var wave = new WaveSpawner(0, 13 + (int)(i * 1.38f));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.64f);
                criteria.MaxEnemiesAlive = 91;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 49;

        }
    }
}
