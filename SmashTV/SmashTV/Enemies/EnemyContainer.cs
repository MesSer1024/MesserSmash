using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Modules;
using SharedSmashResources.Patterns;
using MesserSmash.Commands;

namespace MesserSmash.Enemies {
    public class EnemyContainer : IObserver {
        public delegate void EnemyDelegate(IEnemy enemy);

        private List<IEnemy> _enemies;        
        private float _timeCounter;

        public EnemyContainer() {
            _enemies = new List<IEnemy>();
            _timeCounter = 0;
        }

        public void addEnemy(IEnemy enemy) {
            _enemies.Add(enemy);
        }

        public void update(float deltatime) {
            _timeCounter += deltatime;
            foreach (var i in _enemies) {
                i.preUpdate(deltatime);
                PathFinder.registerPosition(i);
            }
            PathFinder.makeEnemiesNotClumpTogether();

            foreach (var i in _enemies) {
                i.update(deltatime); //do the actual position updates
            }

            if (_timeCounter > 1.5f) {
                removeDestroyedEnemies();
            }
        }

        private void removeDestroyedEnemies() {
            List<IEnemy> destroyedEnemies = _enemies.FindAll(a => a.State == EnemyBase.EnemyStates.Removed);
            foreach (var i in destroyedEnemies) {
                _enemies.Remove(i);
            }
            _drawEnemiesAlive();
            _timeCounter = 0;
        }

        public void drawBegin(SpriteBatch sb) {
            foreach (var enemy in _enemies) {
                enemy.drawBegin(sb);
            }
        }

        public void draw(SpriteBatch sb) {
            foreach (var enemy in _enemies.FindAll(a => a.IsAlive == false)) {
                enemy.draw(sb);
            }
            foreach (var enemy in getAliveEnemies()) {
                enemy.draw(sb);
            }
        }

        private void _drawEnemiesAlive() {
            InfoWindow._aliveEnemies = getAliveEnemies().Count.ToString();
        }

        public List<IEnemy> getAliveEnemies() {
            return _enemies.FindAll(a => a.IsAlive);
        }

        internal List<IEnemy> getEngagingEnemies() {
            return _enemies.FindAll(a => a.State == EnemyBase.EnemyStates.EngagingPlayer);
        }

        public void endGame() {
            var enemies = getAliveEnemies();
            foreach (IEnemy enemy in enemies) {
                enemy.onArenaEnded();
            }
        }

        public void handleCommand(Commands.ICommand cmd) {
            switch (cmd.Name) {
                case ReloadDatabaseCommand.NAME:
                    foreach (var item in getAliveEnemies()) {
                        item.reloadDatabaseValues();
                    }
                    break;
            }
        }

        public void clear() {
            _enemies.Clear();
        }
    }
}
