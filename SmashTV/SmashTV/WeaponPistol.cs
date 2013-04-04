using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;

namespace MesserSmash {
    public class WeaponPistol {
        private const float SHOT_GENERATION_CD = 0.140f;
        private float _timeSinceLastShot;


        public WeaponPistol() {
            _timeSinceLastShot = 0;
        }

        public void update(float gametime) {
            _timeSinceLastShot += gametime;
        }

        public void tryFireShot(Vector2 pos, Vector2 targetPos) {
            if (canFireShot()) {
                fire(pos, targetPos);
            }
        }

        private void fire(Vector2 pos, Vector2 targetPos) {
            if (EnergySystem.Instance.couldFirePistolShot()) {
                Vector2 dir = targetPos - pos;
                if (dir == Vector2.Zero)
                    dir = new Vector2(1, 0);
                dir.Normalize();
                Vector2 myPos = pos + dir * 18;
                new PistolShot(myPos, dir);
                _timeSinceLastShot = 0;
                new PlaySoundCommand(AssetManager.getWeaponSound()).execute();
            }
        }

        public bool canFireShot() {
            if (_timeSinceLastShot >= SHOT_GENERATION_CD)
                if (EnergySystem.Instance.canFirePistolShot())
                    return true;
            return false;
        }
    }
}
