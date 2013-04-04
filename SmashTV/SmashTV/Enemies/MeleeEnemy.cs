﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MesserSmash.Behaviours;
using MesserSmash.Modules;

namespace MesserSmash.Enemies {
    public class MeleeEnemy : EnemyBase {

        public MeleeEnemy(Vector2 position, Player player) {
            Position = position;
            Damage = DataDefines.ID_MELEE_ENEMY_DAMAGE;
            _target = player;
        }

        protected override float _getRadius() {
            return DataDefines.ID_MELEE_ENEMY_RADIUS;
        }

        protected override Texture2D _getTexture() {
            return AssetManager.getEnemyTexture();
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackingMeleeBehaviour(_position);
            //behaviour.onReachedPlayer += onReachedPlayer;
            behaviour.onBehaviourEnded += onAttackDone;
            return behaviour;
        }

        private void onReachedPlayer(Behaviour behaviour) {
            //if (EnemyReachedPlayer != null) {
            //    EnemyReachedPlayer.Invoke(this);
            //}
        }
    }
}
