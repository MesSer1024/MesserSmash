using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO;

namespace MesserSmash.Commands {
    class SaveGameCommand : Command {
        public static readonly string NAME = "SaveGameCommand";
        private StatusUpdate _states;

        public SaveGameCommand(SharedSmashResources.StatusUpdate states)
            : base(NAME) {
                _states = states;

                var folder = new DirectoryInfo(System.Environment.CurrentDirectory);
                folder = folder.CreateSubdirectory("games");
                
                var timestamp = DateTime.Now.Ticks.ToString();
                using (var sw = new StreamWriter(folder.FullName + "/" + timestamp + "_save.txt")) {
                    sw.Write(_states.toJson());
                    sw.Flush();
                }
        }
    }
}
