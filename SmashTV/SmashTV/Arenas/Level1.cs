using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    public class Level1 : Arena {
        private float _timestampLastSpawnedWave;
        private float _internalWaveTimer;
        private int _spawnCounter;

        public Level1() {
            _secondsLeft = 60;
        }

        protected override List<Spawnpoint> createSpawnpoints() {
            
            var list = new List<Spawnpoint>();

            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X - 140, Bounds.Bottom - 60, 60, 60),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Center.X + 100, Bounds.Top, 60, 60),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Left, Bounds.Center.Y, 60, 60),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Right - 60, Bounds.Center.Y, 60, 60),
                                    TextureManager.getArenaTexture()));
            return list;
        }

        public override void startLevel() {
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 1");
            _timestampLastSpawnedWave = DataDefines.ID_LEVEL1_TIME_BETWEEN_WAVES > 1 ? DataDefines.ID_LEVEL1_TIME_BETWEEN_WAVES : 0;
        }

        protected override void custUpdate(float gametime) {
            _timestampLastSpawnedWave += gametime;
            if (_timestampLastSpawnedWave >= DataDefines.ID_LEVEL1_TIME_BETWEEN_WAVES) {
                _internalWaveTimer += gametime;
                if (_internalWaveTimer >= DataDefines.ID_LEVEL1_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter < DataDefines.ID_LEVEL1_MAX_ENEMIES_PER_WAVE) {
                    getRandomSpawnpoint().generateMeleeEnemies(1);
                    _internalWaveTimer = 0;
                    _spawnCounter++;
                } else if (_spawnCounter >= DataDefines.ID_LEVEL1_MAX_ENEMIES_PER_WAVE) {
                    _spawnCounter = 0;
                    _internalWaveTimer = 0;
                    _timestampLastSpawnedWave = 0;
                }
            }

        }
    }
}
