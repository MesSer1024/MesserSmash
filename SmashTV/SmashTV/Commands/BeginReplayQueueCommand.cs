using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using SharedSmashResources;
using System.IO;

namespace MesserSmash.Commands {
    class BeginReplayQueueCommand : Command {
        public const string NAME = "BeginReplayQueueCommand";

        public List<FileInfo> Replays {
            get;
            private set;
        }

        public BeginReplayQueueCommand()
            : base(NAME) {
        }

        protected override void custExecute() {
            Replays = new List<FileInfo>();
            var files = Directory.GetFiles(System.Environment.CurrentDirectory + "/" + SmashTVSystem.Instance.ReplayPath, "*.mer", SearchOption.AllDirectories);
            foreach (var fileString in files) {
                FileInfo file = new FileInfo(fileString);
                if (file.Exists && !file.Name.StartsWith("last_")) {
                    Replays.Add(file);
                }
            }
        }
    }
}
