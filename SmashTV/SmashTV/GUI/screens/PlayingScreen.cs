using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using SharedSmashResources.Patterns;
using MesserSmash.Commands;

namespace MesserSmash.GUI.screens {
    class PlayingScreen : IScreen, IObserver {
        private FunnyText _recharge;
        private float _timeRechargeShown;

        public void initialize() {
            Controller.instance.addObserver(this);
        }

        public void destroy() {
            Controller.instance.removeObserver(this);
        }

        public void update(float deltatime) {
            if (_recharge != null) {
                _timeRechargeShown += deltatime;
                if (_timeRechargeShown >= 0.75f) {
                    _recharge = null;
                }
            }
        }

        public void draw(Microsoft.Xna.Framework.Graphics.SpriteBatch sb) {
            var text = new FunnyText(String.Format("Level {0}", GUIState.Level), new Rectangle { X = GUIState.PortraitPosition.Right + Utils.calcResolutionScaledValue(117), Y = 0, Width = 600, Height = 50 });
            text.HorizontalCenter = false;
            text.Font = AssetManager.getGuiFont();
            text.TextScale = 1.25f;
            text.TextColor = Color.WhiteSmoke;
            sb.Draw(AssetManager.getDefaultTexture(), GUIState.Background, GUIState.BackgroundColor);
            sb.Draw(AssetManager.getPortraitTexture(), GUIState.PortraitPosition, Color.White);
            GUIState.SecondsLeft.Draw(sb);
            var r = new Rectangle(GUIState.Health.Bounds.X, GUIState.Health.Bounds.Y, GUIState.Health.Bounds.Width, GUIState.Health.Bounds.Height);
            var pos = SmashTVSystem.Instance.Player.Position;
            r.X = (int)(pos.X - GUIState.Health.Bounds.Width / 2);
            r.Y = (int)(pos.Y - SmashTVSystem.Instance.Player.Radius - r.Height);
            GUIState.Health.Bounds = r;
            GUIState.Health.draw(sb);
            r.Y += r.Height;
            GUIState.Energy.Bounds = r;
            GUIState.Energy.draw(sb);
            GUIState.KillText.Draw(sb);
            GUIState.KillCounter.Draw(sb);
            GUIState.SecondsLeftMiddleScreenCounter.Draw(sb);
            GUIState.ScoreField.Draw(sb);
            text.Draw(sb);
            if (_recharge != null) {
                _recharge.Draw(sb);
            }
        }

        public void handleCommand(Commands.ICommand cmd) {
            switch (cmd.Name) {
                case LauncherReadyCommand.NAME:
                    onLauncherReady(cmd as LauncherReadyCommand);
                    break;
            }
        }

        private void onLauncherReady(LauncherReadyCommand cmd) {
            _recharge = new FunnyText("Weapon Recharged!", new Rectangle(0, 0, Utils.getGameWidth(), Utils.getGameHeight()));
            _recharge.TextColor = Color.NavajoWhite;
            _timeRechargeShown = 0;
        }
    }
}
