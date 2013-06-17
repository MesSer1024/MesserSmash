using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class LevelWonCommand : Command {
        public const string NAME = "LevelWonCommand";
        public SmashTVSystem GameInstance { get; set; }
        public GUI.GUIMain Gui { get; set; }
        
        public int Level { get; private set; }
        public int ScoreOnLevel { get; private set; }

        public LevelWonCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
            Level = SmashTVSystem.Instance.Arena.Level;
            ScoreOnLevel = (int)Scoring.getLevelScore();
            Gui.showLevelWon(GameInstance.GameHighscores, false, Level, ScoreOnLevel);
            new RequestLevelHighscoresCommand(GameInstance.Arena.Level, GameInstance.GameHighscores, onRoundResponse).execute();
        }

        private void onRoundResponse(RequestLevelHighscoresCommand cmd) {
            if (!SmashTVSystem.IsGameStarted) {
                var scoringProvider = SmashTVSystem.Instance.GameHighscores;
                scoringProvider.addHighscores(cmd.Scores);
                Gui.showLevelWon(scoringProvider, true, Level, ScoreOnLevel);
            } else {
                Logger.error("Game started before LevelWonCommand was finished with highscores");
            }
        }

    }
}
