using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MesserSmashWebServer {
    class MesserSmashUser {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ActiveToken { get; set; }
        public long TimeRegistered { get; set; }
    }
}
