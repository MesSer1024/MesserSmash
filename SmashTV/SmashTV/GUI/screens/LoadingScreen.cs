using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MesserSmash.GUI.screens {
    class LoadingScreen : IScreen {
        public void initialize() {

        }

        public void update(float deltatime) {
            if (!SmashTVSystem.Instance.WaitingForGameCredentials && Utils.isNewKeyPress(Keys.Space)) {
                performClientReady();
            }
        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
            sb.Draw(AssetManager.getControlsTexture(), new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight()), Color.White);

            if (SmashTVSystem.Instance.WaitingForGameCredentials) {
                var text = new FunnyText(String.Format("Using Internet..."), new Rectangle { X = 0, Y = Utils.getGameHeight() - 100, Width = Utils.getGameWidth(), Height = 0 });
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
            } else {
                var text = new FunnyText(String.Format("Press <space> to start Level{0}", SmashTVSystem.Instance.Arena.Level), new Rectangle { X = 0, Y = Utils.getGameHeight() - 100, Width = Utils.getGameWidth(), Height = 0 });
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
            }
        }

        public void destroy() {
        }

        private void performClientReady() {
            new ClientReadyCommand().execute();
        }
    }
}
