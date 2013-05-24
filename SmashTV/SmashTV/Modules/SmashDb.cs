using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Linq.Expressions;
using System.Reflection;
using System.Globalization;

namespace MesserSmash.Modules {

    public static class SmashDb {
        private static Dictionary<int, double> _db;
        //private static List<

        public static void populateKeyValues(StreamReader sr) {
            _db = new Dictionary<int, double>();
            while (!sr.EndOfStream) {
                var line = sr.ReadLine().Split('|');
                var type = "number";
                switch (type.ToLower()) {
                    case "number":
                        parseNumber(line);
                        break;
                    default:
                        throw new Exception("Invalid parse value!");
                }
            }
        }

        private static void parseNumber(string[] line) {
            if (line.Length != 3) {
                return;
            }
            int hashkey = Int32.Parse(line[0]);
            double value = Double.Parse(line[2], NumberStyles.Float, CultureInfo.InvariantCulture);
            _db.Add(hashkey, value);                
        }

        internal static double get(int hash) {
            return _db[hash];
        }

        internal static void set(int hash, double value) {
            _db[hash] = value;
        }

        internal static string getLevel(int index) {
            return "";
        }
    }
}
