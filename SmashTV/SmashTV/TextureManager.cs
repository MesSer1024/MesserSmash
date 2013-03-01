using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MesserSmash {
    static class TextureManager {
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
        public static Texture2D _rangedEnemy;
        public static SpriteFont _bigGuiFont;

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
    }
}
