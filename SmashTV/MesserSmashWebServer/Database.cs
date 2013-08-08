using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace MesserSmashWebServer {
    class Database {
        private string dbUrl = "./users/all_users.txt";
        private Dictionary<string, MesserSmashUser> _db;

        public Database() {
            reloadDatabase();
        }

        public bool isValidLogin(string username, string password) {
            if(_db.ContainsKey(username)) {
                var user = _db[username];
                if (user.Password == password) {
                    return true;
                }
            }
            return false;
        }

        public void reloadDatabase() {
            var fi = new FileInfo(dbUrl);
            if (!fi.Directory.Exists) {
                fi.Directory.Create();
            }
            if (fi.Exists) {
                using (StreamReader sr = new StreamReader(dbUrl)) {
                    var s = sr.ReadToEnd();
                    _db = JsonConvert.DeserializeObject<Dictionary<string, MesserSmashUser>>(s);
                }
            } else {
                _db = new Dictionary<string, MesserSmashUser>();
            }
        }

        public void dumpDatabase() {
            using (StreamWriter stream = new StreamWriter(dbUrl, false)) {
                stream.Write(JsonConvert.SerializeObject(_db));
                stream.Flush();
            }
        }

        public MesserSmashUser getUser(string userName) {
            return _db[userName];
        }

        public MesserSmashUser createUser(string userName, string password) {
            var user = new MesserSmashUser();
            _db.Add(userName, user);
            user.UserName = userName;
            user.UserId = Guid.NewGuid().ToString();
            user.TimeRegistered = DateTime.Now.Ticks;
            user.ActiveToken = Guid.NewGuid().ToString();
            user.Password = password;
            return user;
        }
    }
}
