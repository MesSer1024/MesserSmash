using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Commands {
    class UpdateHighscoresCommand : Command {
        public const string NAME = "UpdateHighscoresCommand";

        public UpdateHighscoresCommand()
            : base(NAME) {
            Logger.info("ctor() UpdateGameCredentialsCommand");

        }

        protected override void custExecute() {
            Logger.info("Executing UpdateHighscoresCommand");
        }
    }
}

