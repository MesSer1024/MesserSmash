using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Commands;
using SharedSmashResources.Patterns;

namespace MesserSmash.Modules {
    class SoundManager : IObserver {
        private Microsoft.Xna.Framework.Audio.SoundEffectInstance _music;
        
        public void init() {
            Controller.instance.addObserver(this);
            //EventDispatcher
            //Controller.instance.addObserver(LevelStartedCommand.NAME, onLevelStarted);
            //Controller.instance.addObserver(PlaySoundCommand.NAME, onPlaySound);
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
                command.Sound.Play(command.Volume, 0, 0);
            }
        }

        public void handleCommand(ICommand cmd) {
            switch (cmd.Name) {
                case LevelStartedCommand.NAME:
                    onLevelStarted(cmd);
                    break;
                case PlaySoundCommand.NAME:
                    onPlaySound(cmd);
                    break;

            }
        }
    }
}
