using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Commands;

namespace MesserSmash {
    public class WeaponRocketLauncher {
        private const float SHOT_GENERATION_CD = 8.140f;
        private float _timeSinceLastShot;

        public WeaponRocketLauncher() {
            _timeSinceLastShot = SHOT_GENERATION_CD;
        }

        public void update(float gametime) {
            _timeSinceLastShot += gametime;
        }

        public void tryFireShot(Vector2 pos, Vector2 targetPos) {
            if (noCooldown()) {
                if (EnergySystem.Instance.canFireRocket()) {
                    fireShot(pos, targetPos);
                }
            }
        }

        private void fireShot(Vector2 pos, Vector2 targetPos) {
            if (EnergySystem.Instance.couldFireRocket()) {
                new RocketShot(pos, targetPos);
                _timeSinceLastShot = 0;
            }
        }

        public bool noCooldown() {
            if (_timeSinceLastShot >= SHOT_GENERATION_CD)
                return true;
            return false;
        }
    }
}
