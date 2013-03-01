using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MesserSmash.Behaviours;

namespace MesserSmash.Enemies {

    public class MeleeStabber : EnemyBase {

        public MeleeStabber(Vector2 position, Player player) {
            Position = position;
            Damage = 20;
            _target = player;
        }

        protected override float _getRadius() {
            return 8;
        }

        protected override float _getAttackRadius() {
            return 140;
        }

        protected override Texture2D _getTexture() {
            return TextureManager.getEnemyTexture();
        }

        protected override Behaviour createAttackBehaviour() {
            var behaviour = new AttackWithCharge();
            behaviour.onBehaviourEnded += onAttackDone;
            return behaviour;
        }
    }
}
