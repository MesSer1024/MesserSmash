using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmash {
    class EnergySystem {
        private static EnergySystem _instance;
        private float _energyAvailable;
        private float _maxEnergy;
        private const float REGENERATION_RATE = 10;

        private const float COST_FIRE_ROCKET = 15f;
        private const float COST_FIRE_PISTOL_SHOT = 1.05f;
        private const float COST_ACTIVATE_SPRINT_MODE = 5f;
        private const float COST_RUNNING = 100/2.5f; //should be depleted over "value" seconds

        public float AvailableEnergy { get { return _energyAvailable; } }
        public float MaxEnergy { get { return _maxEnergy; } }


        public static EnergySystem Instance {
            get {
                if (_instance == null) {
                    _instance = new EnergySystem();
                }
                return _instance;
            }
        }
        private EnergySystem() {
            _maxEnergy = 100;
            _energyAvailable = _maxEnergy;
        }

        public void update(float deltatime) {
            _energyAvailable += REGENERATION_RATE * deltatime;
            if (_energyAvailable > _maxEnergy) {
                _energyAvailable = _maxEnergy;
            }
        }

        public bool couldFireRocket() {
            if (_energyAvailable >= COST_FIRE_ROCKET) {
                _energyAvailable -= COST_FIRE_ROCKET;
                return true;
            }
            return false;
        }

        public bool canFireRocket() {
            if (_energyAvailable >= COST_FIRE_ROCKET)
                return true;
            return false;
        }

        public bool couldFirePistolShot() {
            _energyAvailable -= COST_FIRE_PISTOL_SHOT;
            if (_energyAvailable < 0)
                _energyAvailable = 0;
            return true;
        }

        public bool canActivateSprintMode() {
            return _energyAvailable >= COST_ACTIVATE_SPRINT_MODE;
        }

        public bool couldRun(float deltaTime) {
            float value = COST_RUNNING * deltaTime;
            if (value <= _energyAvailable) {
                _energyAvailable -= value;
                return true;
            }
            return false;
        }

        public bool canRun(float deltatime) {
            return COST_RUNNING * deltatime <= _energyAvailable;
        }

        public bool canFirePistolShot() {
            return true;
        }

        internal void reset() {
            _maxEnergy = 100;
            _energyAvailable = _maxEnergy;
        }
    }
}
