using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SharedSmashResources {
    public class GameLoader {
        public StatusUpdate Replay { get; private set;}

        public GameLoader(string url, bool inGame) {
            var path = inGame ? "./games/" : "../../../../bin/debug/games/";
            using (var sr = new StreamReader(path + url)) {
                var s = sr.ReadToEnd();
                //var state = fastJSON.JSON.Instance.Parse(s);
                //state[""]
                Replay = fastJSON.JSON.Instance.ToObject<StatusUpdate>(s);
                //var foo = new StatusUpdate();
                //StatusUpdate status = fastJSON.JSON.Instance.FillObject(new StatusUpdate(), s) as StatusUpdate;
                //var keyboardStates = state.KeyboardStates;
                //foreach (var keyb in keyboardStates) {
                //    if (keyb.GetPressedKeys().Length > 0) {
                //        var keys = keyb.GetPressedKeys();
                //    }
                //}
            }
        }
    }
}
