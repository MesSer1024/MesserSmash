using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Enemies;
using MesserSmash.Arenas;
using SharedSmashResources.Patterns;
using MesserSmash.Commands;
using MesserSmash.Modules;

namespace MesserSmash.AI
{
    class AIManager : IObserver
    {
        private EnemyContainer _enemies;
        private Arena _arena;
        private List<WaveSpawner> _waves;
        public float BudgetCurrent { get; set; }
        public float BudgetOriginal { get; set; }

        private int _priceMeleeUnit = 30;
        private int _priceRangeUnit = 55;
        private int _priceSecondaryMeleeUnit = 70;

        public AIManager(EnemyContainer container, Arena arena)
        {
            _enemies = container;
            _arena = arena;

            _waves = new List<WaveSpawner>();
            BudgetOriginal = BudgetCurrent = 2000;
            int totalEnemies = (int)(BudgetOriginal / _priceMeleeUnit);
            int n = (int)_arena.SecondsToFinish - 5;

            //select a random pattern of how to distribute enemies
            //adhere to the rules of level (which is budget and what types of enemies to spawn and how many of each type (percentage))

            //[0] = 15%
            //[5-20] = 15%
            //[20-30] = 25%
            //[30-40] = 5%
            //[40-55] = 40%
            {
                var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, (int)(totalEnemies * 0.15f));
                _waves.Add(wave);
            }

            {
                var iterations = Utils.randomInt(2) + 2;
                int iterationTotal = (int)(totalEnemies * 0.15f);
                int enemyCounter = 0;
                for (int i = 0; i < iterations; i++)
                {
                    var range = Utils.randomInt(20 - 5) + 5;
                    var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, iterationTotal/iterations);
                    wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = range });
                    _waves.Add(wave);
                    enemyCounter += wave.SpawnCount;
                }
                if (iterations > 0)
                {
                    var idx = _waves.Count - iterations + Utils.randomInt(iterations);
                    _waves[idx].SpawnCount += (iterationTotal - enemyCounter);
                }
            }

            {
                //[20-30] = 25%
                var iterations = Utils.randomInt(8) + 2;
                int iterationTotal = (int)(totalEnemies * 0.25f);
                int enemyCounter = 0;
                for (int i = 0; i < iterations; i++)
                {
                    var range = Utils.randomInt(30-20) + 20;
                    var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, iterationTotal / iterations);
                    wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = range });
                    _waves.Add(wave);
                    enemyCounter += wave.SpawnCount;
                }
                if (iterations > 0)
                {
                    _waves[_waves.Count - 1].SpawnCount += (iterationTotal - enemyCounter);
                }
            }

            {
                //[30-40] = 5%
                var iterations = Utils.randomInt(4) + 2;
                int iterationTotal = (int)(totalEnemies * 0.05f);
                int enemyCounter = 0;
                for (int i = 0; i < iterations; i++)
                {
                    var range = Utils.randomInt(40 - 30) + 30;
                    var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, iterationTotal / iterations);
                    wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = range });
                    _waves.Add(wave);
                    enemyCounter += wave.SpawnCount;
                }
                if (iterations > 0)
                {
                    _waves[_waves.Count - 1].SpawnCount += (iterationTotal - enemyCounter);
                }
            }

            {
                //[40-55] = 40%
                var iterations = Utils.randomInt(7) + 2;
                int iterationTotal = (int)(totalEnemies * 0.40f);
                int enemyCounter = 0;
                for (int i = 0; i < iterations; i++)
                {
                    var range = Utils.randomInt(56-40) + 40;
                    var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, iterationTotal / iterations);
                    wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = range });
                    _waves.Add(wave);
                    enemyCounter += wave.SpawnCount;
                }
                if (iterations > 0)
                {
                    _waves[_waves.Count - 1].SpawnCount += (iterationTotal - enemyCounter);
                }
            }

            //var wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 4);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 10 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 10);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 0 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 30);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 15 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 30);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 20 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 30);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 30 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 30);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 40 });
            //_waves.Add(wave);

            //wave = new WaveSpawner((int)EnemyTypes.Types.Melee, 100);
            //wave.addCriteria(new SpawnCriteria() { MinSecondsInArena = 50 });
            //_waves.Add(wave);

        }

        public void update(GameState state)
        {
            if (_arena.State == Arena.States.Running)
            {
                foreach (var wave in _waves)
                {
                    wave.update(state);
                }
            }
        }


        public void handleCommand(Commands.ICommand cmd)
        {
            switch (cmd.Name)
            {
                case LevelStartedCommand.NAME:
                    _arena.getRandomSpawnpoint().generateSecondaryRangedEnemies(4);
                    _arena.getRandomSpawnpoint().generateSecondaryRangedEnemies(4);
                    break;
            }
        }
    }
}
