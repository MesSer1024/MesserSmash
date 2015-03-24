using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;
using Microsoft.Xna.Framework.Input;

namespace MesserSmash.GUI.screens {
    class GameOverScreen : IScreen {
        private float _eorTimer;
        public void initialize() {
            _eorTimer = 0;
        }

        public void destroy() {
        }

        public void update(float deltatime) {
            _eorTimer += deltatime;
            if (_eorTimer > 1.0f && !SmashTVSystem.Instance.WaitingForGameCredentials) {
                if (Utils.isNewKeyPress(Keys.R)) {
                    new RestartGameCommand(1).execute();
                } else if (Utils.isNewKeyPress(Keys.Enter)) {
                    new RestartGameCommand(GUIState.Level).execute();
                }
            }

        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
            var r = new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight());
            sb.Draw(AssetManager.getDefaultTexture(), r, Color.Black);

            {
                var text = new FunnyText("GAME OVER!", new Rectangle { X = 50, Y = 0, Width = Utils.getGameWidth(), Height = 75 });
                text.HorizontalCenter = false;
                text.Draw(sb);
            }
            {
                var text = new FunnyText(Utils.makeString("Your Score On Level: {0} RoundScore: {1}", Utils.formatScorePoints(GUIState.ScoreLevel), Utils.formatScorePoints(GUIState.ScoreRound)), new Rectangle { X = 50, Y = 75, Width = Utils.getGameWidth(), Height = 75 });
                text.HorizontalCenter = false;
                text.Draw(sb);
            }
            {
                var text = new FunnyText(Utils.makeString("Press <enter> to retry level"), new Rectangle(50, 200, Utils.getGameWidth(), 75));
                text.HorizontalCenter = false;
                text.Draw(sb);
            }
            {
                var text = new FunnyText(Utils.makeString("Press <r> to restart game"), new Rectangle(50, 250, Utils.getGameWidth(), 75));
                text.HorizontalCenter = false;
                text.Draw(sb);
            }

            var highscores = GUIState.ScoringProvider.getMergedHighscoresOnRound(SmashTVSystem.Instance.RoundId);
            for (int i = 0; i < Math.Min(GUIState.MAX_HIGHSCORES_TO_SHOW, highscores.Count); ++i) {
                var item = highscores[i];
                var prefix = item.IsLocalHighscore || item.IsVerified == false ? "*" : "";
                var foo = new FunnyText(String.Format("{3}{0} - {1} - {2}", item.UserName, item.Score, item.Kills, prefix), new Rectangle(100, 300 + 50 * i, Utils.getGameWidth(), 75));
                foo.HorizontalCenter = false;
                foo.Draw(sb);
            }
        }
    }
}
