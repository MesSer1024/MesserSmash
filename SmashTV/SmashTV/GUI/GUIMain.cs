using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MesserSmash.Commands;
using MesserSmash.Modules;
using SharedSmashResources;
using SharedSmashResources.Patterns;

namespace MesserSmash.GUI {
    class GUIMain : IObserver {
        private const int MAX_HIGHSCORES_TO_SHOW = 8;
        public static GUIMain Instance { get; set; }
        private DebugGuiOverlay _debugGui;
        private bool _popupVisible;
        private string _popupText;
        private Action _onPopupCallback;

        private GUIState _state;

        public GUIMain() {
            Instance = this;
            _debugGui = new DebugGuiOverlay(new Rectangle(Utils.calcResolutionScaledValue(60), Utils.calcResolutionScaledValue(60), Utils.calcResolutionScaledValue(1275), Utils.calcResolutionScaledValue(900)));
            _state = new GUIState();
            Controller.instance.addObserver(this);
            reset();
        }

        public void startLevel() {
            reset();
            GUIState.Level = SmashTVSystem.Instance.Arena.Level;
            GUIState.Background = new Rectangle(Utils.calcResolutionScaledValue(443), Utils.calcResolutionScaledValue(0), Utils.calcResolutionScaledValue(1033), Utils.calcResolutionScaledValue(89));
            //GUIState.PortraitPosition = 
            _state.changeState(GuiStates.InGame);
        }

        public void reset() {
            _popupVisible = false;
            _onPopupCallback = null;
        }

        public void update(float gametime) {
            _state.update(gametime);

            if (_popupVisible) {
                if (Utils.isNewKeyPress(Keys.Space)) {
                    _popupVisible = false;
                    if (_onPopupCallback != null) {
                        _onPopupCallback.Invoke();
                    }
                    return;
                }
            }
        }

        public void performClientReady() {
            new ClientReadyCommand().execute();
        }
        private bool validHighscoreCharacter(Keys key) {
            return (key >= Keys.A && key <= Keys.Z) || (key >= Keys.D0 && key <= Keys.D9);
        }

        public void setPlayerHealth(float curr, float max) {
            GUIState.Health.Value = curr/max;
        }

        public void setPlayerEnergy(float curr, float max) {
            GUIState.Energy.Value = curr / max;
        }

        public void setKillCount(int value) {
            GUIState.KillCounter.Text = value.ToString();
        }

        public void draw(SpriteBatch sb) {
            if(_state != null)
                _state.draw(sb);

            if (_popupVisible) {
                sb.Draw(AssetManager.getDefaultTexture(), Utils.getGameBounds(), Color.Black * 0.6f);
                sb.Draw(AssetManager.getDefaultTexture(), new Rectangle(300, 200, 600, 400), Color.Black);
                var text = new FunnyText(String.Format("{0}\nPress <space> to close", _popupText), Utils.getGameBounds());
                text.HorizontalCenter = true;
                text.VerticalCenter = true;
                text.Draw(sb);
            }

        }

        public void setSecondsLeft(float time) {
            if (Utils.valueBetween(time, 0, 9)) {
                //use countdown in middle of screen
                
                GUIState.SecondsLeftMiddleScreenCounter.Text = ((int)time).ToString();
                GUIState.SecondsLeftMiddleScreenCounter.Visible = true;
                //scale : [1..3]    timespan :  [10..1]
                var numeric = time/10;
                GUIState.SecondsLeftMiddleScreenCounter.TextScale = (1 - numeric) * 2 + 1;
            } else {
                GUIState.SecondsLeftMiddleScreenCounter.Visible = false;
            }

            GUIState.SecondsLeft.Text = String.Format("Seconds: {0}", time > 0 ? (int)time : 0);
        }

        public void setScore(float newScore) {
            GUIState.ScoreRound = (int)newScore;
            GUIState.ScoreField.Text = Utils.makeString("{0}", Utils.formatScorePoints(GUIState.ScoreRound, true));
        }

        internal void showLoadingScreen() {
            Logger.info("showLoadingScreen");
            _state.changeState(GuiStates.LoadingLevel);
        }

        internal void showOkPopup(string s, Action cb) {
            if (_popupVisible) {
                Logger.error("Allready showing a popup, create a new solution for popups...");
            }
            _popupVisible = true;
            _popupText = s;
            _onPopupCallback = cb;
        }

        internal void showNextReplayOnClick() {
            showOkPopup("Replay Finished, continue to next?", onShowNextReplay);
        }

        private void onShowNextReplay() {
            new RestartGameCommand(-10).execute();
        }

        public void handleCommand(ICommand cmd) {
            switch (cmd.Name) {
                case LevelWonCommand.NAME:
                    onLevelWon(cmd as LevelWonCommand);
                    break;
                case RoundWonCommand.NAME:
                    onRoundWon(cmd as RoundWonCommand);
                    break;
            }
        }

        private void onLevelWon(LevelWonCommand cmd) {
            GUIState.Level = cmd.Level;
            GUIState.ScoringProvider = cmd.GameInstance.GlobalHighscores;
            GUIState.ScoreLevel = cmd.ScoreOnLevel;

            _state.changeState(GuiStates.EndOfLevel);
        }

        private void onRoundWon(RoundWonCommand cmd) {
            GUIState.Level = SmashTVSystem.Instance.Arena.Level;
            GUIState.ScoreLevel = cmd.ScoreOnLevel;
            GUIState.ScoringProvider = cmd.GameInstance.GlobalHighscores;

            _state.changeState(GuiStates.EndOfRound);
        }

        public void showGameOver(HighscoreContainer scoringProvider, bool gotScoreData, int scoreOnLevel) {
            GUIState.ScoreLevel = scoreOnLevel;
            GUIState.Level = SmashTVSystem.Instance.Arena.Level;
            GUIState.ScoringProvider = scoringProvider;

            _state.changeState(GuiStates.GameOver);
        }
    }
}
