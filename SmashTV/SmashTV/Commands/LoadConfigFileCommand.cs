using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MesserSmash.Commands {
    class LoadConfigFileCommand : Command {
        public const string NAME = "LoadConfigFileCommand";

        public bool HasUsername { get; set; }
        public string Username { get; set; }

        public LoadConfigFileCommand()
            : base(NAME) {
            HasUsername = false;

            StringBuilder sb = new StringBuilder();
            using (var sr = new StreamReader("./settings.ini")) {
                while (!sr.EndOfStream) {
                    var splitter = '|';
                    var line = sr.ReadLine().Trim();
                    if (line.StartsWith("username")) {
                        var foo = line.Split(splitter);
                        var item = foo[1].Trim();
                        if (item.Length > 0 && item != "$_foo") {
                            HasUsername = true;
                            Username = item;
                        }
                    }
                }
            }
        }
    }
}
