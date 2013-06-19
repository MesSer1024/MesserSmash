using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
namespace MesserSmash.Arenas {
    public class Level20 : Arena {

        public Level20() {
            _secondsLeft = 60;
            Level = 20;

            //TODO Boss Level
            WaveSpawner wave = new WaveSpawner((int)EnemyTypes.Types.Range, 333);
            var criteria = new SpawnCriteria();
            criteria.MinSecondsInArena = 5;
            wave.addCriteria(criteria);
            _spawners.Add(wave);
        }

        protected override void custUpdate(GameState state) {
            if (state.EnemiesAlive == 0 && state.TimeInArena > 5) {
                handleArenaCompleted();
            }
        }
    }
}
