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
        float Score { get; }

        void init();
        void reloadDatabaseValues();

        void takeDamage(float amount);
        void onPlayerInAttackRadius();
        void onArenaEnded();

        void preUpdate(float deltatime);
        void update(float deltatime);
        void drawBegin(SpriteBatch sb);
        void draw(SpriteBatch sb);
    }
}
