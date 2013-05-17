using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections;

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

        public List<MesserKeys> KeyboardStates { get; set; }
        public List<MesserMouse> MouseStates { get; set; }
        public List<float> DeltaTimes { get; set; }

        public StatusUpdate() {
            //TODO: Need to handle KeyboardStates and MouseStates manually in order to parse them to json, 
            //they can't be parsed automatically and returns as "all 0 values"
            KeyboardStates = new List<MesserKeys>();
            MouseStates = new List<MesserMouse>();
            DeltaTimes = new List<float>();
        }

        public void addKeyboard(MesserKeys keyb) {
            KeyboardStates.Add(keyb);
            //StringBuilder sb = new StringBuilder();
            //int i = 0;
            //foreach (var key in keyb.GetPressedKeys()) {
            //    if (i++>0) {
            //        sb.Append(",");
            //    }
            //    sb.Append(key.ToString());
            //}
            //var keys = keyb.GetPressedKeys();
            //if (keys.Length > 0) {
            //    KeyboardStates.Add(keys[0]);
            //} else {
            //    KeyboardStates.Add(Keys.None);
            //}
            //KeyboardStates.Add(keys);
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
            UserName = "";
            UserId = "";
            GameId = "";
        }

        public String toJson() {
            StringBuilder sb = new StringBuilder();

            sb.Append(fastJSON.JSON.Instance.ToJSON(this));
            return sb.ToString();
        }

        public int StoredStatesCount { get { return MouseStates.Count; } }
    }
}
