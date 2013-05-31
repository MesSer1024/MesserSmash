using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level10 : Arena {

        public Level10() {
            _secondsLeft = 60;
            Level = 10;

            //TODO Boss Level
            WaveSpawner wave = new WaveSpawner((int)EnemyTypes.Types.Range, 333);
            var criteria = new SpawnCriteria();
            criteria.MinSecondsInArena = 5;
            criteria.MaxEnemiesAlive = 30;
            wave.addCriteria(criteria);
            _spawners.Add(wave);
        }
    }
}
