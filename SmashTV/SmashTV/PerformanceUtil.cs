using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace MesserSmash {
    class PerformanceUtil {
        private static Dictionary<String, int> _timers = new Dictionary<string,int>();

        internal static void begin(string p) {
            if (!_timers.ContainsKey(p)) {
                _timers.Add(p, System.Environment.TickCount);
            } else {
                _timers[p] = System.Environment.TickCount;
            }
        }

        internal static void end(string p) {
            var delta = System.Environment.TickCount - _timers[p];
        }
    }
}
