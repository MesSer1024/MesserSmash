using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {
    public class EnemyContainer {
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
            Logger.info("Enemy count: {0}", _enemies.Count);
            PerformanceUtil.begin("pre_update");
            foreach (var i in _enemies) {
                i.preUpdate(deltatime);
                PathFinder.registerPosition(i);
            }
            PerformanceUtil.end("pre_update");
            PerformanceUtil.begin("clump");
            PathFinder.makeEnemiesNotClumpTogether();
            PerformanceUtil.end("clump");

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

        public void draw(SpriteBatch sb) {
            foreach (var i in _enemies.FindAll(a => a.IsAlive == false)) {
                i.draw(sb);
            }
            foreach (var i in _enemies.FindAll(a => a.IsAlive)) {
                i.draw(sb);
            }
        }

        private void _drawEnemiesAlive() {
            InfoWindow._aliveEnemies = _enemies.Count.ToString();
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
    }
}
