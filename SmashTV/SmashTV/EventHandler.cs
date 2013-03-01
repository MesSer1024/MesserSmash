using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash {
    class EventHandler {
        public delegate void GameDelegate(object sender, string description);
        private static EventHandler _instance;
        private Dictionary<GameEvent.GameEvents, List<GameDelegate>> _listeners;

        private EventHandler() {
            //if (_instance == null) {
            //    _instance = new EventHandler();
            
            _listeners = new Dictionary<GameEvent.GameEvents,List<GameDelegate>>();
        }

        public static EventHandler Instance  {
            get { if (_instance == null) _instance = new EventHandler(); 
                return _instance; }
        }

        public void addEventListener(GameEvent.GameEvents e, GameDelegate callback) {
            if (_listeners.ContainsKey(e) == false) {
                _listeners.Add(e, new List<GameDelegate>());
            }
            _listeners[e].Add(callback);
        }

        public void dispatchEvent(GameEvent.GameEvents e, object sender, string description) {
            if (_listeners.ContainsKey(e)) {
                foreach (var i in _listeners[e]) {
                    i.Invoke(sender, description);
                }
            }
        }

        public void removeEventListener(GameEvent.GameEvents e, GameDelegate callback) {
            if (_listeners.ContainsKey(e) && _listeners[e].Exists(a => a == callback)) {
                _listeners[e].Remove(callback);
            }
        }
    }
}
