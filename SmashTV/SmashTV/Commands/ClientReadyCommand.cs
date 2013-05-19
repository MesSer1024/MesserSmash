using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class ClientReadyCommand : Command {
        public const string NAME = "ClientReadyCommand";

        public ClientReadyCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
        }
    }
}

