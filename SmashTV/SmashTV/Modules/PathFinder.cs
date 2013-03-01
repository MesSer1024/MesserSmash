using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Enemies;
using Microsoft.Xna.Framework;

namespace MesserSmash.Modules {
    public static class PathFinder {
        //enemy, original position, wanted position, radius [from enemy?]

        private static List<IEnemy> _enemies = new List<IEnemy>();
        private static Dictionary<IEnemy, int> _collisionCounts = new Dictionary<IEnemy,int>();

        public static void registerPosition(IEnemy enemy) {
            _enemies.Add(enemy);
            if (!_collisionCounts.ContainsKey(enemy)) {
                _collisionCounts.Add(enemy, 0);
            }
        }

        public static void makeEnemiesNotClumpTogether() {
            _enemies = _enemies.FindAll(a => a.CollisionEnabled).Distinct().ToList(); //remove duplicates and unwanted items
            //random LINQ and LAMBDA code
            var manyCollisions = from i in _collisionCounts
                                 where i.Value > 2
                                 select i.Key;

            //do something so they dont "clump together that much"
            //easy solution [hack]
            slowdownOverlappingEntities();
            //moveAwayFromEachOther();

            //better solution - implement Potential Fields [node based map where each "selection/part/pixel" has a "busy value" and units try to avoid busy values

            
            //lastly, clear so we can redo next frame...
            _enemies.Clear();
            _collisionCounts.Clear();
        }

        private static void moveAwayFromEachOther() {
            IEnemy iEnemy;
            IEnemy jEnemy;
            for (int i = 0; i < _enemies.Count; ++i) {
                iEnemy = _enemies[i];
                var ix = iEnemy.Position + iEnemy.Velocity;
                var avoidance = Vector2.Zero;
                for (int j = i + 1; j < _enemies.Count; ++j) {
                    jEnemy = _enemies[j];
                    var jx = jEnemy.Position + jEnemy.Velocity;

                    var dy = ix.X * jx.Y - jx.X * ix.Y;
                    var dx = ix.X * jx.X + ix.Y * jx.Y;
                    var angleBetween = Math.Atan2(dy, dx);
                    avoidance += new Vector2((float)Math.Cos(angleBetween) * 2, (float)Math.Sin(angleBetween) * 2);
                    //jEnemy.Velocity *= avoidance;
                }
                iEnemy.Velocity += avoidance * 0.015f;
            }
        }

        private static void slowdownOverlappingEntities() {
            IEnemy iEnemy;
            IEnemy jEnemy;

            //okay, basic idea here is following, we don't want to move units around randomly just because they happen to "clump together"
            //and we dont want to have every unit clump together in a BIG FAT STACK/PILE either, so my idea is the following
            //we find out which entities that are overlapping
            //then we go through all these overlapping entities and between the two that are overlapping, the one "furthest away from the player" will be slowed down
            //then we do this iteratively so an item that is overlapping 5 others etc gets slowed down more than others etc
            //this will make units move around and avoid stacking on top of each other

            for (int i = 0; i < _enemies.Count; ++i) {
                iEnemy = _enemies[i];
                for (int j = i + 1; j < _enemies.Count; ++j) {
                    jEnemy = _enemies[j];

                    if (overlaps(iEnemy, jEnemy)) {
                        var distPlayerE1 = SmashTVSystem.Instance.Player.Position - iEnemy.Position;
                        var distPlayerE2 = SmashTVSystem.Instance.Player.Position - jEnemy.Position;

                        //tag the entity furthest away from player as a collision (so only that one is slowed down and the other can continue at full speed)
                        if (distPlayerE1.LengthSquared() > distPlayerE2.LengthSquared()) {
                            _collisionCounts[iEnemy] += 1;
                        } else {
                            _collisionCounts[jEnemy] += 1;
                        }
                    }
                }
            }

            foreach (var i in _collisionCounts.Keys) {
                int counts = _collisionCounts[i];
                if (counts > 0) {

                    //slow down the units with many overlaps
                    float minimumVelocityFactor = 0.075f; // value between 0..1
                    float velocityReductionDuringOverlap_percentage = 0.165f;
                    float factor = Math.Max(minimumVelocityFactor, 1 - counts * velocityReductionDuringOverlap_percentage);
                    i.Velocity *= factor;
                }
            }
        }

        private static bool overlaps(IEntity e1, IEntity e2) {
            var delta = e1.Position + e1.Velocity - (e2.Position + e2.Velocity);
            return delta.LengthSquared() < (e1.Radius + e2.Radius) * (e1.Radius + e2.Radius);
        }

        private static void tagFurthestAwayFromPlayer(IEntity e1, IEntity e2) {
            var distPlayerE1 = SmashTVSystem.Instance.Player.Position - e1.Position;
            var distPlayerE2 = SmashTVSystem.Instance.Player.Position - e2.Position;

            if (distPlayerE1.LengthSquared() > distPlayerE2.LengthSquared()) {
                _collisionCounts[e1 as IEnemy] += 1;
            } else {
                _collisionCounts[e2 as IEnemy] += 1;
            }
        }

        private static void randomlyFreezeOneEntity(IEntity e1, IEntity e2) {
            if (Utils.randomBool()) {
                e1.Velocity = Vector2.Zero;
            } else {
                e2.Velocity = Vector2.Zero;
            }
        }
    }
}
