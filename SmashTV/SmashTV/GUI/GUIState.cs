using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MesserSmash.GUI.screens;

namespace MesserSmash.GUI {
    public enum GuiStates {
        Frontend,
        LoadingLevel,
        InGame,
        EndOfLevel,
        EndOfRound,
        GameOver,
    }

    class GUIState {
        public const int MAX_HIGHSCORES_TO_SHOW = 8;

        public static int Level { get; set; }
        public static Rectangle Background;
        public static Rectangle PortraitPosition;
        public static Color BackgroundColor;
        public static FunnyText KillText;
        public static FunnyText SecondsLeft;
        public static FunnyText KillCounter;
        public static Healthbar Health;
        public static Healthbar Energy;
        public static FunnyText ScoreField;
        public static FunnyText SecondsLeftMiddleScreenCounter;
        public static int ScoreLevel;
        public static int ScoreRound;

        private static GUIState _instance;
        private GuiStates _state = GuiStates.Frontend;
        private IScreen _screen;
        public static SharedSmashResources.HighscoreContainer ScoringProvider;

        public GUIState() {
            if (_instance != null)
                throw new Exception("Expected class not to be instantiated twice");
            _instance = this;
            Background = new Rectangle(Utils.calcResolutionScaledValue(443), Utils.calcResolutionScaledValue(0), Utils.calcResolutionScaledValue(1033), Utils.calcResolutionScaledValue(89));
            BackgroundColor = new Color(14,14,14);
            
            KillText = new FunnyText("Kills", new Rectangle(Background.Left + Utils.calcResolutionScaledValue(186), 0, Utils.calcResolutionScaledValue(80), Utils.calcResolutionScaledValue(35)));
            KillText.TextScale = Utils.getResolutionScale();
            KillText.TextColor = Color.WhiteSmoke;
            KillText.Font = AssetManager.getGuiFont();

            SecondsLeft = new FunnyText("Seconds", new Rectangle(Background.Right - 100, 0, 100, Background.Height));
            SecondsLeft.HorizontalCenter = false;
            SecondsLeft.TextScale = Utils.getResolutionScale();
            SecondsLeft.TextColor = Color.WhiteSmoke;
            SecondsLeft.Font = AssetManager.getGuiFont();

            KillCounter = new FunnyText("", new Rectangle(Background.Left + Utils.calcResolutionScaledValue(186), Utils.calcResolutionScaledValue(35), Utils.calcResolutionScaledValue(80), Utils.calcResolutionScaledValue(35)));
            KillCounter.TextScale = Utils.getResolutionScale();
            KillCounter.TextColor = Color.WhiteSmoke;
            KillCounter.Font = AssetManager.getGuiFont();

            PortraitPosition = new Rectangle(KillCounter.Bounds.Right, 0, Utils.calcResolutionScaledValue(80), Utils.calcResolutionScaledValue(80));
            var barWidth = 65;
            var barHeight = 5;
            Health = new Healthbar(new Rectangle(PortraitPosition.Right + 5, PortraitPosition.Center.Y - 23, barWidth, barHeight));
            Energy = new Healthbar(new Rectangle(PortraitPosition.Right + 5, Health.Bounds.Bottom + 6, barWidth, barHeight));
            Health._valueColor = Color.Yellow;
            Energy._valueColor = Color.LightBlue;

            var h = Utils.calcResolutionScaledValue(88);
            ScoreField = new FunnyText("0", new Rectangle { X = 0, Y = Utils.getGameHeight() - h, Width = Utils.calcResolutionScaledValue(1920), Height = h });
            ScoreField.Font = AssetManager.getGuiFont();
            ScoreField.TextScale = Utils.getResolutionScale();
            ScoreField.TextColor = Color.WhiteSmoke;

            //var gameScreen = SmashTVSystem.Instance.Arena.Bounds; //arena not initialized...
            var scale = Utils.getResolutionScale();
            var gameScreen = new Rectangle((int)(322 * scale), (int)(90 * scale), (int)(1275 * scale), (int)(900 * scale)); // default impl inside arena
            SecondsLeftMiddleScreenCounter = new FunnyText("100", new Rectangle(gameScreen.Left, gameScreen.Top, gameScreen.Width, gameScreen.Height));
            SecondsLeftMiddleScreenCounter.TextColor = Color.LightGoldenrodYellow;
            SecondsLeftMiddleScreenCounter.Visible = false;
            SecondsLeftMiddleScreenCounter.TextScale = 2.25f;

            changeState(GuiStates.LoadingLevel);
        }

        public void changeState(GuiStates state) {
            _state = state;
            if (_screen != null) {
                _screen.destroy();
            }
            _screen = createScreen(state);
            _screen.initialize();
        }

        private IScreen createScreen(GuiStates state) {
            switch (state) {
                case GuiStates.Frontend:
                    return new FrontendScreen();
                case GuiStates.EndOfLevel:
                    return new EndOfLevelScreen();
                case GuiStates.EndOfRound:
                    return new EndOfRoundScreen();
                case GuiStates.GameOver:
                    return new GameOverScreen();
                case GuiStates.InGame:
                    return new PlayingScreen();
                case GuiStates.LoadingLevel:
                    return new LoadingScreen();
            }
            throw new Exception();
        }

        public void update(float deltatime) {
            _screen.update(deltatime);
        }

        public void draw(SpriteBatch sb) {
            _screen.draw(sb);
        }
    }
}
