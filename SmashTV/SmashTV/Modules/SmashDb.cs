using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Reflection;

namespace MesserSmash.Modules {

    public static class SmashDb {
        private static Dictionary<int, double> _db;

        public static void populate(StreamReader sr) {
            _db = new Dictionary<int, double>();
            var foo = sr.ReadLine().Split(':');
            _db.Add(generateKey(foo[0]), 0);
        }

        private static int generateKey(string s) {
            return s.GetHashCode();
        }

        internal static double get(int hash) {
            return _db[hash];
        }
    }
}
