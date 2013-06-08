using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash.Commands {
    class UpdateGameCredentialsCommand : Command {
        public const string NAME = "UpdateGameCredentialsCommand";

        public string SessionId { get; set; }
        public string GameId { get; set; }

        public UpdateGameCredentialsCommand(string session, string gameid)
            : base(NAME) {
            Logger.info("ctor() UpdateGameCredentialsCommand");
            SessionId = session ?? "";
            GameId = gameid ?? "";
        }

        protected override void custExecute() {
            Logger.info("Executing UpdateGameCredentialsCommand");
        }

    }
}
