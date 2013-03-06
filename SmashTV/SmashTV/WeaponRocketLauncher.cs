using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MesserSmash {
    public class WeaponRocketLauncher {
        private const float SHOT_GENERATION_CD = 6.140f;
        private float _timeSinceLastShot;

        public WeaponRocketLauncher() {
            _timeSinceLastShot = SHOT_GENERATION_CD;
        }

        public void update(float gametime) {
            _timeSinceLastShot += gametime;
        }

        public void tryFireShot(Vector2 pos, Vector2 targetPos) {
            if (canFireShot()) {
                fireShot(pos, targetPos);
            }
        }

        private void fireShot(Vector2 pos, Vector2 targetPos) {
            if (EnergySystem.Instance.couldFireRocket()) {
                new RocketShot(pos, targetPos);
                _timeSinceLastShot = 0;
            }
        }

        public bool canFireShot() {
            if (_timeSinceLastShot >= SHOT_GENERATION_CD)
                if(EnergySystem.Instance.canFireRocket())
                    return true;
            return false;
        }
    }
}
