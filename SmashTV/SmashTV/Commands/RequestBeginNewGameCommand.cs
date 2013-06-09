using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using SharedSmashResources;
using System.IO;

namespace MesserSmash.Commands {
    class RequestBeginNewGameCommand : Command {
        public const string NAME = "RequestBeginNewGameCommand";

        public int Level { get; private set; }

        public RequestBeginNewGameCommand(int level)
            : base(NAME) {

            Level = level;

            ThreadWatcher.runBgThread(() => {
                var server = new LocalServer(SmashTVSystem.Instance.ServerIp);
                var dir = new Dictionary<string, object> {
                    {MesserSmashWeb.LEVEL, level},
                    {MesserSmashWeb.LOGIN_SESSION, SmashTVSystem.Instance.LoginResponseKey},
                    {MesserSmashWeb.USER_NAME, SmashTVSystem.Instance.Username},
                    {MesserSmashWeb.USER_ID, SmashTVSystem.Instance.UserId}
                };

                server.requestBeginGame(onResponse, dir);
            });
        }

        private void onResponse(int rc, string data) {
            Logger.info("RequestSessionCommand cb");
            if (rc == 0) {
                try {
                    var dir = MesserSmashWeb.toObject(data);
                    var gameid = dir[MesserSmashWeb.GAME_ID];
                    var sessionid = dir[MesserSmashWeb.SESSION_ID];

                    var cmd = new UpdateGameCredentialsCommand(sessionid.ToString(), gameid.ToString());
                    cmd.execute();
                } catch (System.Exception ex) {
                    Logger.error("Error parsing RequestSession data={0}", ex.ToString());
                    invalidData();
                }
            } else {
                invalidData();
            }
        }

        private void invalidData() {            
            new UpdateGameCredentialsCommand(null, null).execute();
        }
    }
}
