using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MesserSmash.Modules;
using SharedSmashResources;

namespace MesserSmash.Commands {
    class SaveGameCommand : Command {
        public const string NAME = "SaveGameCommand";
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
                Logger.flushToFile(folder.FullName + "/" + timestamp + "_log.txt");

                using (var sw = new StreamWriter(folder.FullName + "/last_save.txt", false)) {
                    sw.Write(_states.toJson());
                    sw.Flush();
                }

                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                server.send(_states);
        }
    }
}
