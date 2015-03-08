using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Enemies;
using Microsoft.Xna.Framework;

namespace MesserSmash.Modules {
    public static class PathFinder {
        private static List<IEnemy> _enemies = new List<IEnemy>();
        private static Dictionary<IEnemy, int> _collisionCounts = new Dictionary<IEnemy,int>();

        public static void registerEnemy(IEnemy enemy) {
            _enemies.Add(enemy);
            if (!_collisionCounts.ContainsKey(enemy))
                _collisionCounts.Add(enemy, 0);
        }

        public static void spreadEnemies() {
            _enemies = _enemies.FindAll(a => a.CollisionEnabled).Distinct().ToList(); //remove unwanted items (non collidable entities)

            //do something so they dont "clump together that much"
            //better solution - implement Potential Fields [node based map where each "selection/part/pixel" has a "busy value" and units try to avoid busy values
            slowdownOverlappingEntities();


            _enemies.Clear();
            _collisionCounts.Clear();
        }

        private static void slowdownOverlappingEntities() {
            //okay, basic idea here is following, we don't want to move units around randomly just because they happen to "clump together"
            //and we dont want to have every unit clump together in a BIG FAT STACK/PILE either, so my idea is the following
            //we find out which entities that are overlapping
            //then we go through all these overlapping entities and between the two that are overlapping, the one "furthest away from the player" will be slowed down
            //then we do this iteratively so an item that is overlapping 5 others etc gets slowed down more than others etc
            //this will make units move around and avoid stacking on top of each other

            IEnemy iEnemy;
            IEnemy jEnemy;
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

        public static bool overlaps(IEntity e1, IEntity e2) {
            var delta = e1.Position + e1.Velocity - (e2.Position + e2.Velocity);
            return delta.LengthSquared() < (e1.Radius + e2.Radius) * (e1.Radius + e2.Radius);
        }
    }
}
