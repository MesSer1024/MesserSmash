using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MesserSmash.Commands {
    class RegisterUsernameCommand : Command {
        public const string NAME = "RegisterUsernameCommand";

        public bool HasUsername { get; set; }
        public string Username { get; set; }

        public RegisterUsernameCommand(string username)
            : base(NAME) {
            HasUsername = true;
            Username = username;

            StringBuilder sb = new StringBuilder();
            using (var sw = new StreamWriter("./settings.ini", true)) {
                sw.WriteLine(Utils.makeString("\nusername | {0}", username));
                sw.Flush();
            }
        }
    }
}
