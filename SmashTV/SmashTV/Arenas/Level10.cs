using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
using Microsoft.Xna.Framework;
namespace MesserSmash.Arenas {
    public class Level10 : Arena {

        public Level10() {
            _secondsLeft = float.MaxValue;
            Level = 10;

        }

        public override void custStartLevel() {
            getRandomSpawnpoint().spawnEnemy(new SpawnBoss1(Vector2.Zero, SmashTVSystem.Instance.Player));
        }

        protected override void custUpdate(GameState state) {
            if (state.EnemiesAlive == 0 && state.TimeInArena > 5) {
                handleArenaCompleted();
            }
        }
    }
}
