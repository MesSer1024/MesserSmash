using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace SharedSmashResources {
    public class StatusUpdate {
        public string GameId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        
        public int Seed { get; set; }
        public int Level { get; set; }
        public int Kills { get; set; }
        public int Score { get; set; }
        
        public double Energy { get; set; }
        public double TotalGametime { get; set; }
        public double TimeMultiplier { get; set; }

        public List<KeyboardState> KeyboardStates { get; set; }
        public List<MouseState> MouseStates { get; set; }
        public List<float> DeltaTimes { get; set; }

        public StatusUpdate() {
            KeyboardStates = new List<KeyboardState>();
            MouseStates = new List<MouseState>();
            DeltaTimes = new List<float>();
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
            UserName = "";
            UserId = "";
            GameId = "";
        }

        public String toJson() {
            StringBuilder sb = new StringBuilder();

            sb.Append(fastJSON.JSON.Instance.ToJSON(this));
            return sb.ToString();
        }
    }
}
