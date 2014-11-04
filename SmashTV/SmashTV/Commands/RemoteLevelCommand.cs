using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash.Commands {
    class RemoteLevelCommand : Command {
        public const string NAME = "RemoteLevelCommand";
        public string Data { get; private set; }

        public RemoteLevelCommand(string data)
            : base(NAME) {
            Data = data;
        }
    }
}
