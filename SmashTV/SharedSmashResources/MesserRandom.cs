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
            return nextInt(0, max);
        }

        public static int nextInt(int min, int max) {
            var value = _random.Next(min, max);
            reportStatus(_counter, value.ToString());
            _counter++;
            return value;
        }

        public static double next() {
            var value = _random.NextDouble();
            reportStatus(_counter, value.ToString("0.000"));
            _counter++;
            return value;
        }

        public static bool nextBool() {
            var value = next() < 0.5;
            return value;
        }

        private static void reportStatus(int counter, string value) {
            Logger.info("random [{0}]={1}", counter, value);
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
