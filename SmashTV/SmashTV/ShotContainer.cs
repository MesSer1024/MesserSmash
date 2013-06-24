using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;

namespace MesserSmash {
    public class ShotContainer {
        public ShotContainer() {
            _shots = new List<ShotBase>();
            _enemyShots = new List<ShotBase>();
            _explosions = new List<Explosion>();
            _timeCounter = 0;
        }

        private List<ShotBase> _shots;
        private List<ShotBase> _enemyShots;
        private List<Explosion> _explosions;
        private float _timeCounter;

        public void addShot(ShotBase shot) {
            _shots.Add(shot);
            shot.GenerateGroundEffect += new ShotBase.ShotDelegate(onGenerateGroundEffect);
        }

        public void addEnemyShot(ShotBase shot) {
            _enemyShots.Add(shot);
            shot.GenerateGroundEffect += new ShotBase.ShotDelegate(onGenerateGroundEffect);
        }

        void onGenerateGroundEffect(ShotBase shot, float timeDealDamage, float timeToShow) {
            Texture2D texture = AssetManager.getRocketShotTexture();
            shot.GenerateGroundEffect -= onGenerateGroundEffect;
            _explosions.Add(new Explosion(shot.Position, shot.Radius, texture, timeDealDamage, timeToShow));
        }

        public void update(float deltatime) {
            _timeCounter += deltatime;
            Rectangle bounds = SmashTVSystem.Instance.Arena.Bounds;
            foreach (var i in _shots) {
                i.update(deltatime);
                if (i.CollisionEnabled) {
                    if (isOutOfBounds(i, bounds)) {
                        //new PlaySoundCommand(AssetManager.getWallHitSound()).execute();
                        i.entityCollision(getImpactPosition(i, bounds));
                    }
                }
            }

            foreach (var i in _enemyShots) {
                i.update(deltatime);
                if (i.CollisionEnabled) {
                    if (isOutOfBounds(i, bounds)) {
                        //new PlaySoundCommand(AssetManager.getWallHitSound()).execute();
                        i.entityCollision(getImpactPosition(i, bounds));
                    }
                }
            }

            foreach (var i in _explosions) {
                i.update(deltatime);
            }

            if (_timeCounter > 1.5) {
                removeDestroyedEntities();
            }
        }

        private bool isOutOfBounds(ShotBase shot, Rectangle bounds) {
            if (shot.Position.X <= bounds.X || shot.Position.Y <= bounds.Y)
                return true;
            if (shot.Position.X >= bounds.Right || shot.Position.Y >= bounds.Bottom)
                return true;
            return false;
        }

        private Vector2 getImpactPosition(ShotBase shot, Rectangle bounds) {
            Vector2 impact = shot.Position;
            if (shot.Position.X >= bounds.Right)
                impact.X = bounds.Right;
            else if (shot.Position.X <= bounds.Left)
                impact.X = bounds.Left;
            if (shot.Position.Y <= bounds.Top)
                impact.Y = bounds.Top;
            else if (shot.Position.Y >= bounds.Bottom)
                impact.Y = bounds.Bottom;
            return impact;
        }

        private void removeDestroyedEntities() {
            List<ShotBase> destroyedShots = _shots.FindAll(a => a.IsAlive);
            List<Explosion> destroyedExplosions = _explosions.FindAll(a => a.IsAlive == false);
            foreach (var i in destroyedShots) {
                _shots.Remove(i);
            }
            foreach (var i in destroyedExplosions) {
                _explosions.Remove(i);
            }

            destroyedShots = _enemyShots.FindAll(a => a.IsAlive);
            foreach (var i in destroyedShots) {
                _enemyShots.Remove(i);
            }
            
            _drawShotsAlive();
            _timeCounter = 0;
        }

        public void draw(SpriteBatch sb) {
            foreach (var i in _shots) {
                i.draw(sb);
            }
            foreach (var i in _enemyShots) {
                i.draw(sb);
            }
        }

        public void drawExplosions(SpriteBatch sb) {
            foreach (var i in _explosions) {
                i.draw(sb);
            }
        }

        private void _drawShotsAlive() {
            InfoWindow._aliveShots = _shots.Count.ToString() + " :enemy shots: " + _enemyShots.Count.ToString();
        }

        public List<ShotBase> shotsFlaggedForCollision() {
            return _shots.FindAll(a => a.CollisionEnabled);
        }

        public void endGame() {
            foreach (var shot in shotsFlaggedForCollision()) {
                //should I really do something here?
            }
        }

        public List<ShotBase> enemyShotsFlaggedForCollision() {
            return _enemyShots.FindAll(a => a.CollisionEnabled);
        }

        public void clear() {
            foreach (var shot in _enemyShots) {
                shot.GenerateGroundEffect -= onGenerateGroundEffect;
            }
            foreach (var shot in _shots) {
                shot.GenerateGroundEffect -= onGenerateGroundEffect;
            }
            _enemyShots.Clear();
            _shots.Clear();
            _explosions.Clear();
        }
    }
}
