using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MesserSmash.Modules {
    static class ThreadWatcher {
        static List<Thread> _threads = new List<Thread>();

        public static void runBgThread(ThreadStart a) {
            var t = new Thread(a);
            t.IsBackground = true;
            t.Start();
        }
    }
}
