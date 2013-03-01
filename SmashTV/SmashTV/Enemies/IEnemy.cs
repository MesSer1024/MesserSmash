using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MesserSmash.Enemies {
    public interface IEnemy : IEntity {
        EnemyBase.EnemyStates State { get; }
        float AttackRadius { get; }
        float Damage { get; }

        void init();

        void takeDamage(float amount);
        void onPlayerInAttackRadius();
        void onArenaEnded();

        void preUpdate(float deltatime);
        void update(float deltatime);
        void draw(SpriteBatch sb);
    }
}
