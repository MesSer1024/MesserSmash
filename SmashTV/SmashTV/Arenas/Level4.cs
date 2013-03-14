﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;

namespace MesserSmash.Arenas {
    class Level4 : Arena {
    private float _timestampLastSpawnedWave;
        private float _internalWaveTimer;
        private int _spawnCounter;

        public Level4() {
            _secondsLeft = 60;
        }

        public override void startLevel() {
            _timestampLastSpawnedWave = DataDefines.ID_LEVEL4_TIME_BETWEEN_WAVES - 3;
            EventHandler.Instance.dispatchEvent(GameEvent.GameEvents.GameStarted, this, "Level 4");

        }

        protected override List<Spawnpoint> createSpawnpoints() {
            var list = new List<Spawnpoint>();
            int size = 60;
            list.Add(new Spawnpoint(new Rectangle(Bounds.Left, Bounds.Center.Y - size / 2, size, size),
                                    TextureManager.getArenaTexture()));
            list.Add(new Spawnpoint(new Rectangle(Bounds.Right - size, Bounds.Center.Y - size / 2, size, size),
                                                TextureManager.getArenaTexture()));
            return list;
        }

        protected override void custUpdate(float gametime) {
            _timestampLastSpawnedWave += gametime;
            if (_timestampLastSpawnedWave >= DataDefines.ID_LEVEL4_TIME_BETWEEN_WAVES) {
                _internalWaveTimer += gametime;
                if (_internalWaveTimer >= DataDefines.ID_LEVEL4_BETWEEN_EACH_UNIQUE_SPAWN_CD && _spawnCounter  < DataDefines.ID_LEVEL4_MAX_ENEMIES_PER_WAVE) {
                    if (Utils.randomInt(100) < 33) {
                        getRandomSpawnpoint().generateSecondaryMeleeUnits(1);
                        getRandomSpawnpoint().generateSecondaryMeleeUnits(1);
                    } else {
                        getRandomSpawnpoint().generateRangedEnemies(1);
                        getRandomSpawnpoint().generateSecondaryRangedEnemies(1);
                    }
                    _internalWaveTimer = 0;
                    _spawnCounter += 2;
                } else if (_spawnCounter >= DataDefines.ID_LEVEL4_MAX_ENEMIES_PER_WAVE) {
                    _spawnCounter = 0;
                    _internalWaveTimer = 0;
                    _timestampLastSpawnedWave = 0;
                }
            }
        }
    }
}
