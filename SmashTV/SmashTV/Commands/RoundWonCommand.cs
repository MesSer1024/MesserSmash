using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;
using MesserSmash.Modules;
using System.Threading;

namespace MesserSmash.Commands {
    class RoundWonCommand : Command {
        public const string NAME = "RoundWonCommand";
        public SmashTVSystem GameInstance { get; set; }
        public GUI.GUIMain Gui { get; set; }
        
        public uint RoundId { get; private set; }

        public RoundWonCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
            RoundId = SmashTVSystem.Instance.RoundId;

            Gui.showEntireRoundWon(GameInstance.GameHighscores, false);
            new RequestRoundHighscoresCommand(GameInstance.RoundId, GameInstance.GameHighscores, onRoundResponse).execute();
        }

        private void onRoundResponse(RequestRoundHighscoresCommand cmd) {
            Gui.showEntireRoundWon(cmd.ScoringProvider, true);
        }


    }
}
