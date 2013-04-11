using System;
using System.Collections;
using System.Collections.Generic;
using MesserSmash.Commands;

namespace MesserSmash {
    public class Controller {
        public delegate void CommandDelegate(ICommand command);

        private static Controller _instance;
        public static Controller instance {
            get {
                if (_instance == null)
                    _instance = new Controller();
                return _instance;
            }
        }

        private Dictionary<string, List<CommandDelegate>> _interests;
        private Controller() {
            _interests = new Dictionary<string, List<CommandDelegate>>();
        }

        public void registerInterest(string id, CommandDelegate function) {
            if (_interests.ContainsKey(id) == false) {
                _interests.Add(id, new List<CommandDelegate>());
                _interests[id].Add(function);
            } else {
                if (_interests[id].Contains(function) == false) {
                    _interests[id].Add(function);
                }
            }
        }

        public void handleCommand(ICommand cmd) {
            if (_interests.ContainsKey(cmd.Name)) {
                foreach (var i in _interests[cmd.Name]) {
                    i.Invoke(cmd);
                }
            }
        }

        public void removeInterest(string id, CommandDelegate function) {
            if (_interests.ContainsKey(id)) {
                _interests[id].Remove(function);
            }
        }
    }
}
