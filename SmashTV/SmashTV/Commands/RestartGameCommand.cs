using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class RestartGameCommand : Command {
        public static string NAME = "RestartGameCommand";

        public RestartGameCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
        }
    }
}
