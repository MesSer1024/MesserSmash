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
            _music = AssetManager.getBackgroundMusic().CreateInstance();
            _music.IsLooped = true;
            //music.IsLooped = true;
            //music.Volume = 0.8f;
            //music.State;
            _music.Play();
        }

        private void onPlaySound(ICommand cmd) {
            var command = cmd as PlaySoundCommand;
            command.Sound.Play(0.8f,0,0);
        }
    }
}
