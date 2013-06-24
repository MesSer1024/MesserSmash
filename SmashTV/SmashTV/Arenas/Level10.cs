using MesserSmash.Modules;
using MesserSmash.Enemies;
using System;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;
namespace MesserSmash.Arenas {
    public class Level10 : Arena {

        public Level10() {
            _secondsLeft = 135;
            Level = 10;
        }

        public override void custStartLevel() {
            var boss = new SpawnBoss1(Vector2.Zero, SmashTVSystem.Instance.Player);
            boss.OnDamaged += new SpawnBoss1.Boss1Delegate(bossDamaged);
            getRandomSpawnpoint().spawnEnemy(boss);
        }

        void bossDamaged(SpawnBoss1 sender) {
            new PlaySoundCommand(AssetManager.getBossHitSound()).execute();
            Scoring.addScore(sender.ScoreWhenHit);
            SmashTVSystem.Instance.Gui.setScore(Scoring.getTotalScore());
        }

        protected override void custUpdate(GameState state) {
            if (state.EnemiesAlive == 0 && state.TimeInArena > 5) {
                handleArenaEnding();
            }
        }

        protected override void custArenaEnding() {
            Scoring.addScore(800);
            //add some kind of bonus depending on health and time
            var timeScore = this.SecondsToFinish * 7.68f;
            Scoring.addScore(timeScore);

            var healthFactor = SmashTVSystem.Instance.Player.Health / SmashTVSystem.Instance.Player.MaxHealth;
            var healthScore = 356.79f * healthFactor;
            if (healthFactor >= 0.99f) {
                //full health
                healthScore += 150;
            }

            Scoring.addScore(healthScore);
            SmashTVSystem.Instance.Gui.setScore(Scoring.getTotalScore());
        }
    }
}
