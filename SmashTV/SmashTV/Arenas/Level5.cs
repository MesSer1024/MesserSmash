using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    class Level5 : Arena {
        private float _timestampLastSpawnedWave;
        private float _internalWaveTimer;
        private int _spawnCounter;

        public Level5() {
            _secondsLeft = 65;
        }

        public override void startLevel() {
            _timestampLastSpawnedWave = DataDefines.ID_LEVEL5_TIME_BETWEEN_WAVES - 3;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 5");

        }

        protected override List<Spawnpoint> createSpawnpoints() {
            var list = new List<Spawnpoint>();
            int size = 60;
            list.Add(new Spawnpoint(new Rectangle(Bounds.Left, Bounds.Center.Y - size / 2, size, size),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Right - size, Bounds.Center.Y - size / 2, size, size),
                                                TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X - size / 2, Bounds.Top, size, size),
                                                TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X - size / 2, Bounds.Bottom - size, size, size),
                                                TextureManager.getArenaTexture()));
            return list;
        }

        protected override void custUpdate(GameState state) {
            _timestampLastSpawnedWave += state.DeltaTime;
            if (_timestampLastSpawnedWave >= DataDefines.ID_LEVEL5_TIME_BETWEEN_WAVES) {
                _internalWaveTimer += state.DeltaTime;
                if (_internalWaveTimer >= DataDefines.ID_LEVEL5_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter  < DataDefines.ID_LEVEL5_MAX_ENEMIES_PER_WAVE) {
                    getRandomSpawnpoint().generateRandomEnemies(1);
                    _internalWaveTimer = 0;
                    _spawnCounter++;
                } else if (_spawnCounter >= DataDefines.ID_LEVEL5_MAX_ENEMIES_PER_WAVE) {
                    _spawnCounter = 0;
                    _internalWaveTimer = 0;
                    _timestampLastSpawnedWave = 0;
                }
            }
        }
    }
}
