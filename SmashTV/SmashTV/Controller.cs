using System;
using System.Collections;
using System.Collections.Generic;
using MesserSmash.Commands;
using MesserSmash.Modules;
using SharedSmashResources.Patterns;
using System.Threading;

namespace MesserSmash {
    public class Controller {
        private static Controller _instance;
        public static Controller instance {
            get {
                if (_instance == null)
                    _instance = new Controller();
                return _instance;
            }
        }

        private List<IObserver> _observers;
        private int _modificationLocks;
        private bool _needsUpdate = false;
        private List<IObserver> _itemsToAdd;
        private List<IObserver> _itemsToRemove;

        private Controller() {
            _modificationLocks = 0;
            _observers = new List<IObserver>();
            _itemsToAdd = new List<IObserver>(4);
            _itemsToRemove = new List<IObserver>(4);
        }

        public void addObserver(IObserver observer) {
            Logger.info("->addObserver {0}", observer.GetType());
            if (_observers.Contains(observer)) {
                return;
            }

            if (_modificationLocks > 0) {
                _needsUpdate = true;
                _itemsToAdd.Add(observer);
            } else {
                _observers.Add(observer);
            }
            Logger.info("<-addObserver {0}", observer.GetType());
        }

        public void handleCommand(ICommand cmd) {
            Logger.info("->handleCommand {0}", cmd.Name);
            Interlocked.Increment(ref _modificationLocks);
             foreach (var i in _observers) {
                i.handleCommand(cmd);
            }
            Interlocked.Decrement(ref _modificationLocks);
            if (_modificationLocks < 0) {
                throw new Exception("asdf");
            }
            if (_needsUpdate && _modificationLocks == 0) {
                _needsUpdate = false;
                foreach (var i in _itemsToAdd) {
                    if (!_observers.Contains(i)) {
                        _observers.Add(i);
                    }
                }
                _itemsToAdd.Clear();
                foreach (var i in _itemsToRemove) {
                    _observers.Remove(i);
                }
                _itemsToRemove.Clear();
            }
            Logger.info("<--handleCommand {0}", cmd.Name);
        }

        public void removeObserver(IObserver observer) {
            if (_modificationLocks > 0) {
                _needsUpdate = true;
                _itemsToRemove.Add(observer);
            } else {
                _observers.Remove(observer);
            }
        }
    }
}
