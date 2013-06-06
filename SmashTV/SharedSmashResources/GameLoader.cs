using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SharedSmashResources {
    public class GameLoader {
        public GameStates Replay { get; private set;}

        public GameLoader(string url, bool inGame) {
            var path = inGame ? "./games/" : "../../../../bin/debug/games/";
            using (var sr = new StreamReader(path + url)) {
                var s = sr.ReadToEnd();
                Replay = fastJSON.JSON.Instance.ToObject<GameStates>(s);
            }
        }
    }
}
