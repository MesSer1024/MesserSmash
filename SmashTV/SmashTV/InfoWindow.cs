using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MesserSmash {
    static class InfoWindow {
        public static SpriteFont _defaultFont;
        public static string _playerPosition;
        public static string _aliveShots = "";
        public static string _aliveEnemies = "";
        public static string _killCount = "";
        public static string _health = "";
        public static string _timeLeft = "";
        public static string _energy = "";

        private static SpriteBatch _sb;
        public static string _behaviourDestructors;
        public static string _enemyDestructors;
        //public static string _overlappingEnemies;

        public static void draw(SpriteBatch sb) {
            Vector2 pos = Vector2.Zero;
            _sb = sb;
            pos = internalDraw(_defaultFont, "Playerpos:" + _playerPosition, pos, Color.Black);
            pos = internalDraw(_defaultFont, "+Health+: " + _health, pos, Color.Black);
            pos = internalDraw(_defaultFont, _energy, pos, Color.Black);
            pos = internalDraw(_defaultFont, "Active Shots: " + _aliveShots, pos, Color.Black);
            pos = internalDraw(_defaultFont, "Active Enemies: " + _aliveEnemies, pos, Color.Black);
            pos = internalDraw(_defaultFont, "Enemies Killed: " + _killCount, pos, Color.Black);
            pos = internalDraw(_defaultFont, "Time left: " + _timeLeft, pos, Color.Black);
            pos = internalDraw(_defaultFont, "~Behaviour; " + _behaviourDestructors, pos, Color.Black);
            pos = internalDraw(_defaultFont, "~Enemy; " + _enemyDestructors, pos, Color.Black);
            //pos = internalDraw(_defaultFont, "Overlapping enemies: " + _overlappingEnemies, pos, Color.Black);
        }

        private static Vector2 internalDraw(SpriteFont font, string msg, Vector2 currPos, Color color) {
            Vector2 pos = currPos;
            _sb.DrawString(font, msg, currPos, color);
            pos.Y += 15;
            return pos;
        }
    }
}
