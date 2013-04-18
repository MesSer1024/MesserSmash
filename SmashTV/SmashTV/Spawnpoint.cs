using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Enemies;
using MesserSmash.Arenas;

namespace MesserSmash {
    public class Spawnpoint {
        private Rectangle _bounds;
        private Texture2D _texture;

        public Spawnpoint(Rectangle bounds, Texture2D texture) {
            _bounds = bounds;
            _texture = texture;
        }

        public void update(float gametime) {
        }

        public void generateRandomEnemies(int noOfEnemies) {
            int rnd = Utils.randomInt(3);
            for (var i = 0; i < noOfEnemies; ++i) {
                switch (rnd) {
                    case 0:
                        generateMeleeEnemies(1);
                        break;
                    case 1:
                        generateSecondaryMeleeUnits(1);
                        break;
                    case 2:
                        generateSecondaryRangedEnemies(1);
                        break;
                }
            }
        }

        public void generateMeleeEnemies(int amount) {
            while (amount-- > 0) {
                Player player = SmashTVSystem.Instance.Player;
                IEnemy enemy = new MeleeEnemy(Utils.randomPositionWithinRectangle(_bounds), player);
                enemy.init();
            }
        }

        public void draw(SpriteBatch sb) {
            sb.Draw(_texture, _bounds, Color.Orange);
        }

        public void generateSecondaryRangedEnemies(int amount) {
            while (amount-- > 0) {
                Player player = SmashTVSystem.Instance.Player;
                IEnemy enemy = new SecondaryRangedEnemy(Utils.randomPositionWithinRectangle(_bounds), player);
                enemy.init();
            }
        }

        public void generateSecondaryMeleeUnits(int amount) {
            while (amount-- > 0) {
                Player player = SmashTVSystem.Instance.Player;
                IEnemy enemy = new MeleeStabber(Utils.randomPositionWithinRectangle(_bounds), player);
                enemy.init();
            }
        }
    }
}
