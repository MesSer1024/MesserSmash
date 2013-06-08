using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MesserSmash.Modules;

namespace SharedSmashResources {
    public class GameLoader {
        public GameStates Replay { get; private set;}

        public GameLoader() { }

        public GameLoader loadFromRelativePath(string url) {
            var path = "./games/";
            var file = new FileInfo(path + url);
            loadFromFileInfo(file);
            return this;
        }

        public GameLoader loadFromFileInfo(FileInfo file) {
            if (file.Exists) {
                using (var sr = new StreamReader(file.FullName)) {
                    var s = sr.ReadToEnd();
                    Replay = fastJSON.JSON.Instance.ToObject<GameStates>(s);
                }
            } else {
                Logger.error("Could not find Replay file input={0}", file.FullName);
            }
            return this;
        }
    }
}
