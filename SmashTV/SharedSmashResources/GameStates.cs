using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections;

namespace SharedSmashResources {
    public class GameStates {
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public string LoginKey { get; set; }
        public string UserName { get; set; }
        public string GameVersion { get; set; }
        
        public int Seed { get; set; }
        public int Level { get; set; }
        public int Kills { get; set; }
        public int Score { get; set; }
        
        public double Energy { get; set; }
        public double TotalGametime { get; set; }
        public double TimeMultiplier { get; set; }

        public List<MesserKeys> KeyboardStates { get; set; }
        public List<MesserMouse> MouseStates { get; set; }
        public List<float> DeltaTimes { get; set; }
        public int FrameCounts { get { return MouseStates.Count; } }

        public GameStates() {
            KeyboardStates = new List<MesserKeys>();
            MouseStates = new List<MesserMouse>();
            DeltaTimes = new List<float>();
        }

        public void addKeyboard(MesserKeys keyb) {
            KeyboardStates.Add(keyb);
        }

        public void addMouse(MesserMouse mouse) {
            MouseStates.Add(mouse);
        }

        public void reset() {
            KeyboardStates.Clear();
            MouseStates.Clear();
            DeltaTimes.Clear();
            TimeMultiplier = 0;
            TotalGametime = 0;
            Energy = 0;
            Score = 0;
            Kills = 0;
            Level = 0;
            Seed = 0;
            LoginKey = "";
            UserName = "";
            UserId = "";
            GameId = "";
            SessionId = "";
            GameVersion = "";
        }

        public String toJson() {
            return fastJSON.JSON.Instance.ToJSON(this);
        }
    }
}
