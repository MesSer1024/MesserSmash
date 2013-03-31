using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    class Level2 : Arena {
        private float _timestampLastSpawnedWave;
        private float _internalWaveTimer;
        private int _spawnCounter;

        public Level2() {
            _secondsLeft = 60;
        }

        public override void startLevel() {
            _timestampLastSpawnedWave = DataDefines.ID_LEVEL2_TIME_BETWEEN_WAVES - 3;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 2");

        }

        protected override List<Spawnpoint> createSpawnpoints() {
            var list = new List<Spawnpoint>();
            int size = 100;
            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X - size / 2, Bounds.Center.Y - size / 2, size, size),
                                    TextureManager.getArenaTexture()));
            return list;
        }

        protected override void custUpdate(GameState state) {
            _timestampLastSpawnedWave += state.DeltaTime;
            if (_timestampLastSpawnedWave >= DataDefines.ID_LEVEL2_TIME_BETWEEN_WAVES) {
                _internalWaveTimer += state.DeltaTime;
                if (_internalWaveTimer >= DataDefines.ID_LEVEL2_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter < DataDefines.ID_LEVEL2_MAX_ENEMIES_PER_WAVE) {
                    getRandomSpawnpoint().generateRangedEnemies(1);
                    _internalWaveTimer = 0;
                    _spawnCounter++;
                } else if (_spawnCounter >= DataDefines.ID_LEVEL2_MAX_ENEMIES_PER_WAVE) {
                    _spawnCounter = 0;
                    _internalWaveTimer = 0;
                    _timestampLastSpawnedWave = 0;
                }
            }

        }

    }
}
