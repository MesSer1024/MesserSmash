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
        public uint RoundId { get; set; }

        public UpdateGameCredentialsCommand(string session, string gameid, string roundid)
            : base(NAME) {
            Logger.info("ctor() UpdateGameCredentialsCommand");
            SessionId = session ?? "";
            GameId = gameid ?? "";
            uint r = 0;
            if (uint.TryParse(roundid, out r)) {
                RoundId = r;
            } else {
                //#TODO:do some sort of blaha for local games...
                RoundId = (uint)0;
            }
        }

        protected override void custExecute() {
            Logger.info("Executing UpdateGameCredentialsCommand");
        }

    }
}
