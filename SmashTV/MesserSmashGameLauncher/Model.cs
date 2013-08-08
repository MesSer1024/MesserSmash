using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace MesserSmashGameLauncher {
    public class Model : DependencyObject {
        public static string UserName { get; set; }
        public static string UserId { get; set; }
        public static string Version { get; set; }
        public static string Token { get; set; }
        public static bool Online { get; set; }
        public static string ServerIp { get; set; }

        static Model() {
            Instance = new Model();
            UserName = "messer_@hotmail.com";
            UserId = "1002004";
            Version = "0.0007";
            Token = "";
            Online = true;
        }

        public static Model Instance { get; private set; }
    }
}
