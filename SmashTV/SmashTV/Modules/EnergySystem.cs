using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash {
    class EnergySystem {
        private static EnergySystem _instance;
        private float _energyAvailable;
        
        private const float REGENERATION_RATE = 10;

        private const float MAX_ENERGY = 180;

        private const float COST_FIRE_ROCKET = 15f;
        private const float COST_FIRE_ROCKET_REQUIREMENT = 5f;
        private const float COST_FIRE_PISTOL_SHOT = 1.05f;
        private const float COST_ACTIVATE_SPRINT_MODE = 5f;
        private const float COST_RUNNING = 100/2.5f; //should be depleted over "value" seconds
        private bool _endgame;

        private float _timeSinceUsage;

        public float AvailableEnergy { get { return _energyAvailable; } }
        public float MaxEnergy { get { return MAX_ENERGY; } }


        public static EnergySystem Instance {
            get {
                if (_instance == null) {
                    _instance = new EnergySystem();
                }
                return _instance;
            }
        }
        private EnergySystem() {
            _energyAvailable = MAX_ENERGY;
            _timeSinceUsage = 0;
        }

        public void update(float deltatime) {
            if (_endgame)
                return;
            _timeSinceUsage += deltatime;
            _energyAvailable += REGENERATION_RATE * deltatime * getRegenBonus();
            if (_energyAvailable > MAX_ENERGY) {
                _energyAvailable = MAX_ENERGY;
            }
        }

        private float getRegenBonus() {
            var v = Utils.clamp(_timeSinceUsage / 2.55f, 1, 4);
            return v;
        }

        public bool couldFireRocket() {
            if (_energyAvailable >= COST_FIRE_ROCKET_REQUIREMENT) {
                _timeSinceUsage = 0;
                _energyAvailable = Math.Max(_energyAvailable - COST_FIRE_ROCKET, 0);
                return true;
            }
            return false;
        }

        public bool canFireRocket() {
            return _energyAvailable >= COST_FIRE_ROCKET_REQUIREMENT;
        }

        public bool couldFirePistolShot() {
            _energyAvailable = Math.Max(_energyAvailable - COST_FIRE_PISTOL_SHOT, 0);
            _timeSinceUsage = 0;
            return true;
        }

        public bool canActivateSprintMode() {
            if (_endgame)
                return true;
            return _energyAvailable >= COST_ACTIVATE_SPRINT_MODE;
        }

        public bool couldRun(float deltaTime) {
            if (_endgame)
                return true;
            float value = COST_RUNNING * deltaTime;
            if (value <= _energyAvailable) {
                _timeSinceUsage = 0;
                _energyAvailable -= value;
                return true;
            }
            return false;
        }

        public bool canRun(float deltatime) {
            if (_endgame)
                return true;
            return COST_RUNNING * deltatime <= _energyAvailable;
        }

        public bool canFirePistolShot() {
            return true;
        }

        internal void reset() {
            _endgame = false;
            _energyAvailable = MAX_ENERGY;
        }

        internal void endGame() {
            _endgame = true;
        }
    }
}
