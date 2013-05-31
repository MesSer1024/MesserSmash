using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using MesserSmash.Modules;
using MesserSmash.Commands;
using System.Collections;

namespace MesserSmash.Arenas {
    public class Level11 : Arena {

        public Level11() {
            _secondsLeft = 60;
            Level = 11;

            for (int i = 0; i < 20; i++) {
                var wave = new WaveSpawner(0, 13 + (int)(i*1.38f));
                var criteria = new SpawnCriteria();
                criteria.MinSecondsInArena = (int)(i*3.64f);
                criteria.MaxEnemiesAlive = 91;
                wave.addCriteria(criteria);                
                _spawners.Add(wave);
            }

            _spawners[0].SpawnCount = 49;

            //string jsonResult = fastJSON.JSON.Instance.ToJSON(_spawners);
            //var o = fastJSON.JSON.Instance.Parse(jsonResult) as List<Object>;
            //for (int i = 0; i < o.Count; ++i) {
            //    var dic = o[i] as Dictionary<string, object>;
            //    var joijio = dic["Criterias"];
            //}
        }
    }
}
