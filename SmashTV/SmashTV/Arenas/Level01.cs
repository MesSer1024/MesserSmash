using MesserSmash.Modules;
namespace MesserSmash.Arenas {
    public class Level01 : Arena {

        public Level01() {
            _secondsLeft = 60;
            Level = 1;

            for (int i = 0; i < 30; i++) {
                var wave = new WaveSpawner(0, 3 + (int)(i * 0.68f));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i * 3.74f);
                criteria.MaxEnemiesAlive = 25;
                wave.addCriteria(criteria);
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 6;
        }
    }
}
