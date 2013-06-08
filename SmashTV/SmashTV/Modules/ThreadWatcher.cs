using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace MesserSmash.Modules {
    static class ThreadWatcher {
        private delegate void KillTimerDelegate(int threadId);

        private class Worker {
            private Thread _thread;
            public KillTimerDelegate DoneCallback { get; set; }

            public Worker(Thread t) {
                //when done, destroy the timer, otherwise the timerkill will occur before...
                _thread = t;
                t.IsBackground = true;

            }

            public void doWork() {
                Logger.info("ManagedThread with id:{0} started", _thread.ManagedThreadId);
                _thread.Start();
                DoneCallback(_thread.ManagedThreadId);
            }
        }

        static Dictionary<int, Thread> _threads = new Dictionary<int, Thread>();
        static Dictionary<int, Timer> _timers = new Dictionary<int, Timer>();
        static object ThreadWatch = new object();
        /// <summary>
        /// Execute work on another thread, useful for I/O or Network messages
        /// </summary>
        /// <param name="a">What to do, lambda expressions are useful similar to () => {_foobar.doSomething();}</param>
        /// <param name="timeoutMS">timeout in milliseconds before thread is aborted, 0 for no timeout</param>
        public static void runBgThread(ThreadStart a, int timeoutMS = 0) {
            //a.Invoke();
            var t = new Thread(a);
            if (timeoutMS == 0) {
                t.IsBackground = true;
                _threads.Add(t.ManagedThreadId, t);
                t.Start();
            } else {
                var w = new Worker(t);
                w.DoneCallback += onThreadFinished;
                _threads.Add(t.ManagedThreadId, t);
                var timer = new Timer(onTimerCallback, t.ManagedThreadId, 0, timeoutMS);
                _timers.Add(t.ManagedThreadId, timer);
                w.doWork();
            }
        }

        static void onTimerCallback(object threadIdArg) {
            int threadId = (int)threadIdArg;
            if (_threads.ContainsKey(threadId)) {
                Logger.info("Thread with id:{0} aborted", threadId);
                _threads[threadId].Abort();
                killTimer(threadId);
            }
        }

        static void onThreadFinished(int threadId) {
            Logger.info("Thread with id:{0} finished", threadId);
            killTimer(threadId);
        }

        static void killTimer(int threadId) {
            _threads.Remove(threadId);
            _timers[threadId].Dispose();
            _timers.Remove(threadId);
        }

        static void bg_DoWork(object sender, DoWorkEventArgs e) {
            throw new NotImplementedException();
        }


    }
}
