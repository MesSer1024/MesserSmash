using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class GameOverCommand : Command {
        public static string NAME = "GameOverCommand";

        public float Score { get; private set; }
        public GUI.GUIMain Gui { get; private set; }

        public GameOverCommand(GUI.GUIMain gui)
            : base(NAME) {
            Gui = gui;
        }

        protected override void custExecute() {
            Score = Scoring.getScore();
            Gui.setScore(Score);
            Gui.showGameOver();
            Scoring.reset();
        }
    }
}
