using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace MesserSmash.GUI.screens {
    class EndOfRoundScreen : IScreen {
        private float _eorTimer;
        public void initialize() {
            _eorTimer = 0;
        }

        public void update(float deltatime) {
            _eorTimer += deltatime;
            if (_eorTimer > 1.0f && !SmashTVSystem.Instance.WaitingForGameCredentials) {
                if (Utils.isNewKeyPress(Keys.Space)) {
                    GUIMain.Instance.showOkPopup("Game is finished, do something game creator, need gui!", null);
                }
            }

        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
            var r = new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight());
            sb.Draw(AssetManager.getDefaultTexture(), r, Color.Black);

            if (SmashTVSystem.Instance.WaitingForGameCredentials) {
                var text = new FunnyText(String.Format("Using Internet..."), new Rectangle { X = 0, Y = Utils.getGameHeight() - 100, Width = Utils.getGameWidth(), Height = 0 });
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
            } else {
                {
                    var text = new FunnyText("All our base are belong to you (Victorious!!)", new Rectangle { X = 50, Y = 0, Width = Utils.getGameWidth(), Height = 75 });
                    text.HorizontalCenter = false;
                    text.Draw(sb);
                }
                {
                    var text = new FunnyText(Utils.makeString("Your Score On Level: {0} RoundScore: {1}", Utils.formatScorePoints(GUIState.ScoreLevel, false), Utils.formatScorePoints(GUIState.ScoreRound, false)), new Rectangle { X = 50, Y = 75, Width = Utils.getGameWidth(), Height = 75 });
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
                {
                    var text = new FunnyText(String.Format("Press <space> to do something", SmashTVSystem.Instance.Arena.Level), new Rectangle { X = 0, Y = Utils.getGameHeight() - 100, Width = Utils.getGameWidth(), Height = 0 });
                    text.HorizontalCenter = true;
                    text.VerticalCenter = true;
                    text.Draw(sb);
                }
            }
        }

        public void destroy() {
        }

    }
}
