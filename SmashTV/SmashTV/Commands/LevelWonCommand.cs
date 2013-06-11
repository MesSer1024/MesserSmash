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

        public LevelWonCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
            Level = SmashTVSystem.Instance.Arena.Level;
            Gui.showLevelWon(GameInstance.GameHighscores, false, Level);
            new RequestLevelHighscoresCommand(GameInstance.Arena.Level, GameInstance.GameHighscores, onRoundResponse).execute();
        }

        private void onRoundResponse(RequestLevelHighscoresCommand cmd) {
            Gui.showLevelWon(cmd.ScoringProvider, true, Level);
        }


    }
}
