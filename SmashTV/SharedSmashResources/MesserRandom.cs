using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;

namespace SharedSmashResources {
    public static class MesserRandom {
        private static int _seed;
        private static Random _random;
        private static int _counter;

        public static void init(int seed) {
            _counter = 0;
            _seed = seed;
            _random = new Random(seed);
        }

        public static int nextInt(int max) {
            _counter++;
            return nextInt(0, max);
        }

        public static int nextInt(int min, int max) {
            _counter++;
            return _random.Next(min, max);
        }

        public static double next() {
            _counter++;
            return _random.NextDouble();
        }

        public static bool nextBool() {
            _counter++;
            return next() < 0.5;
        }

        public static string getStatus() {
            var sb = new StringBuilder();
            var seed = "" + _seed ?? "n/a";
            var counter = "" + _counter ?? "n/a";
            sb.AppendFormat("MesserRandom seed={0} totalRequests={1}", _seed, _counter);
            return sb.ToString();
        }
        
    }
}
