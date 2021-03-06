﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace MesserSmash {
    static class AssetManager {
        public static Texture2D _shot;
        public static Texture2D _player;
        public static Texture2D _arena;
        public static Texture2D _enemy;
        public static Texture2D _healthPack;
        public static Texture2D _moneyBag;
        public static Texture2D _rocketShot;
        public static Texture2D _default;
        public static Texture2D _playerPortrait;
        public static SpriteFont _defaultFont;
        public static Texture2D _controllerLayout;
        public static Texture2D _rangedEnemy;
        public static SpriteFont _bigGuiFont;
        public static SoundEffect _bgSound;
        public static SoundEffect _failSound;
        public static SoundEffect _weaponSound;
        public static SoundEffect _weaponReadySound;
        public static SpriteFont _guiFont;
        public static SoundEffect _enemyHitSound;
        public static SoundEffect _enemyShootSound;
        public static SoundEffect _bossHitSound;
        public static SoundEffect _enemyChargeSound;
        public static SoundEffect _grenadeImpactSound;
        public static SoundEffect _moneyPickupSound;
        public static SoundEffect _playerHitSound;
        public static SoundEffect _wallHitSound;
        public static SoundEffect _lootDropped;

        public static Texture2D getDefaultTexture() {
            return _default;
        }

        public static Texture2D getShotTexture() {
            return _shot;
        }

        public static Texture2D getPlayerTexture() {
            return _player;
        }

        public static Texture2D getArenaTexture() {
            return _arena;
        }

        public static Texture2D getEnemyTexture() {
            return _enemy;
        }

        public static Texture2D getHealthPackTexture() {
            return _healthPack;
        }

        public static Texture2D getMoneyBagTexture() {
            return _moneyBag;
        }

        public static Texture2D getRocketShotTexture() {
            return _rocketShot;
        }

        internal static Texture2D getPortraitTexture() {
            return _playerPortrait;
        }

        public static SpriteFont getDefaultFont() {
            return _defaultFont;
        }

        public static SpriteFont getBigGuiFont() {
            return _bigGuiFont;
        }

        internal static Texture2D getRangedEnemyTexture() {
            return _rangedEnemy;
        }

        public static SoundEffect getBackgroundMusic() {
            return _bgSound;
        }

        public static SoundEffect getFailSound() {
            return _failSound;
        }

        public static SoundEffect getWeaponSound() {
            return _weaponSound;
        }

        public static SoundEffect getWeaponReadySound() {
            return _weaponReadySound;
        }

        public static Texture2D getControlsTexture() {
            return _controllerLayout;
        }

        public static SpriteFont getGuiFont() {
            return _bigGuiFont;
        }

        public static SoundEffect getEnemyHitSound() {
            return _enemyHitSound;
        }

        public static SoundEffect getEnemyShootSound() {
            return _enemyShootSound;
        }

        public static SoundEffect getBossHitSound() {
            return _bossHitSound;
        }

        public static SoundEffect getEnemyChargeSound() {
            return _enemyChargeSound;
        }

        public static SoundEffect getGrenadeImpactSound() {
            return _grenadeImpactSound;
        }

        public static SoundEffect getMoneyPickupSound() {
            return _moneyPickupSound;
        }

        public static SoundEffect getLootDroppedSound() {
            return _lootDropped;
        }
        
        public static SoundEffect getPlayerHitSound() {
            return _playerHitSound;
        }
        
        public static SoundEffect getWallHitSound() {
            return _wallHitSound;
        }
    }
}
