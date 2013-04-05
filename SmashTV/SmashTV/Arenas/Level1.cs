using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Commands;
using System.Collections;

namespace MesserSmash.Arenas {
    public class Level1 : Arena {
        private float _timestampLastSpawnedWave;
        //private float _internalWaveTimer;
        //private int _spawnCounter;
        private List<WaveSpawner> _spawners = new List<WaveSpawner>();
        public Level1() {
            _secondsLeft = 60;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 20; i++) {
                var wave = new WaveSpawner(0, 13 + (int)(i*1.28f));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i*3.74f);
                criteria.MaxEnemiesAlive = 71;
                wave.addCriteria(criteria);                
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 49;

            //string jsonResult = fastJSON.JSON.Instance.ToJSON(_spawners);
            //var o = fastJSON.JSON.Instance.Parse(jsonResult) as List<Object>;
            //for (int i = 0; i < o.Count; ++i) {
            //    var dic = o[i] as Dictionary<string, object>;
            //    var joijio = dic["Criterias"];
            //}
        }

        protected override List<Spawnpoint> createSpawnpoints() {
            return Utils.generateSpawnpoints(Bounds);
        }

        public override void startLevel() {
            new LevelStartedCommand(this).execute();
            //EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 1");
            _timestampLastSpawnedWave = 0;
        }

        protected override void custUpdate(GameState state) {
            _timestampLastSpawnedWave += state.DeltaTime;
            foreach (var wave in _spawners) {
                wave.update(state);
            }
            //if (_timestampLastSpawnedWave >= DataDefines.ID_LEVEL1_TIME_BETWEEN_WAVES) {
            //    _internalWaveTimer += gametime;
            //    if (_internalWaveTimer >= DataDefines.ID_LEVEL1_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter < DataDefines.ID_LEVEL1_MAX_ENEMIES_PER_WAVE) {
            //        getRandomSpawnpoint().generateMeleeEnemies(1);
            //        _internalWaveTimer = 0;
            //        _spawnCounter++;
            //    } else if (_spawnCounter >= DataDefines.ID_LEVEL1_MAX_ENEMIES_PER_WAVE) {
            //        _spawnCounter = 0;
            //        _internalWaveTimer = 0;
            //        _timestampLastSpawnedWave = 0;
            //    }
            //}
        }

        protected override void custSpawnWaveCommand(WaveSpawner spawner) {
            int enemyType = spawner.EnemyType;
            spawner.deactivate();
            for (int i = 0; i < spawner.SpawnCount; i++) {
                getRandomSpawnpoint().generateMeleeEnemies(1);
            }            
        }
    }
}
