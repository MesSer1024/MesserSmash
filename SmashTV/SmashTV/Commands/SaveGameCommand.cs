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
        private GameStates _states;

        public SaveGameCommand(SharedSmashResources.GameStates states)
            : base(NAME) {
            _states = states;

            var folder = new DirectoryInfo(System.Environment.CurrentDirectory);
            folder = folder.CreateSubdirectory("games");

            var timestamp = DateTime.Now.Ticks.ToString();
            Logger.flushToFile(folder.FullName + "/" + timestamp + "_log.txt");

            ThreadWatcher.runBgThread(() => {
                using (var sw = new StreamWriter(folder.FullName + "/" + timestamp + "_save.mer")) {
                    sw.Write(_states.toJson());
                    sw.Flush();
                }
            });
            ThreadWatcher.runBgThread(() => {
                using (var sw = new StreamWriter(folder.FullName + "/last_save.mer", false)) {
                    sw.Write(_states.toJson());
                    sw.Flush();
                }
            });

            ThreadWatcher.runBgThread(() => {
                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                server.requestSaveGame(_states);
            });
        }
    }
}
