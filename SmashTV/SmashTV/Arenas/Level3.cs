using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MesserSmash.Arenas {
    class Level3 : Arena {
         private float _timestampLastSpawnedWave;
        private float _internalWaveTimer;
        private int _spawnCounter;
        private const int ID_LEVEL3_MAX_ENEMIES_PER_WAVE = 9;
        private const float ID_LEVEL3_TIME_BETWEEN_WAVES = 6;
        private const float ID_LEVEL3_BETWEEN_EACH_UNIQUE_SPAWN_CD = 0.15f;

        public Level3() {
            _secondsLeft = 60;
        }

        public override void startLevel() {
            _timestampLastSpawnedWave = ID_LEVEL3_TIME_BETWEEN_WAVES - 3;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 3");

        }

        protected override List<Spawnpoint> createSpawnpoints() {
            var list = new List<Spawnpoint>();
            int size = 60;
            list.Add(new Spawnpoint(new Rectangle(Bounds.Left, Bounds.Center.Y - size / 2, size, size),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X - size / 2, Bounds.Bottom - size, size, size),
                                                TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Right - size, Bounds.Center.Y - size / 2, size, size),
                                                TextureManager.getArenaTexture()));
            return list;
        }

        protected override void custUpdate(float gametime) {
            _timestampLastSpawnedWave += gametime;
            if (_timestampLastSpawnedWave >= ID_LEVEL3_TIME_BETWEEN_WAVES) {
                _internalWaveTimer += gametime;
                if (_internalWaveTimer >= ID_LEVEL3_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter  < ID_LEVEL3_MAX_ENEMIES_PER_WAVE) {
                    if (Utils.randomBool()) {
                        getRandomSpawnpoint().generateRangedEnemies(1);
                        getRandomSpawnpoint().generateSecondaryRangedEnemies(1);
                    } else {
                        getRandomSpawnpoint().generateMeleeEnemies(1);
                        getRandomSpawnpoint().generateMeleeEnemies(1);
                    }
                    _internalWaveTimer = 0;
                    _spawnCounter += 2;
                } else if (_spawnCounter >= ID_LEVEL3_MAX_ENEMIES_PER_WAVE) {
                    _spawnCounter = 0;
                    _internalWaveTimer = 0;
                    _timestampLastSpawnedWave = 0;
                }
            }

        }
    }
}
