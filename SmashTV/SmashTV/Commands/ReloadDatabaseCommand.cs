using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MesserSmash.Modules;

namespace MesserSmash.Commands {
    class ReloadDatabaseCommand : Command {
        public const string NAME = "ReloadDatabaseCommand";

        public ReloadDatabaseCommand()
            : base(NAME) {

            DirectoryInfo dir = new DirectoryInfo(System.Environment.CurrentDirectory);
            using (StreamReader sr = new StreamReader("./database.txt")) {
                SmashDb.populateKeyValues(sr);
            }
            using (StreamReader sr = new StreamReader("./levels.txt")) {
                SmashDb.populateLevels(sr);
            }
        }
    }
}
