using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;

namespace MesserSmash.Modules {
    class SoundManager {
        private Microsoft.Xna.Framework.Audio.SoundEffectInstance _music;
        public void init() {
            //EventDispatcher
            Controller.instance.registerInterest(LevelStartedCommand.NAME, onLevelStarted);
            Controller.instance.registerInterest(PlaySoundCommand.NAME, onPlaySound);
        }

        private void onLevelStarted(ICommand cmd) {
            var command = cmd as LevelStartedCommand;
            if (DataDefines.ID_SETTINGS_PLAY_MUSIC != 0 && _music == null) {
                _music = AssetManager.getBackgroundMusic().CreateInstance();
                _music.Volume = 0.45f;
                _music.IsLooped = true;
                _music.Play();
            }
        }

        private void onPlaySound(ICommand cmd) {
            var command = cmd as PlaySoundCommand;
            if (DataDefines.ID_SETTINGS_PLAY_SOUND != 0) {
                command.Sound.Play(0.25f, 0, 0);
            }
        }
    }
}
