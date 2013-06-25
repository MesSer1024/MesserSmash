using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using System.Collections;
using Microsoft.Xna.Framework;
using System.IO;
using SharedSmashResources;
using Newtonsoft.Json;

namespace MesserSmashWebServer {
    class MesserSmashTest {

        private static DateTime _start = DateTime.Now;
        public static void test(string[] args) {
            //--begin request--
            var input = JsonConvert.SerializeObject(dummyBeginGameRequest());
            //var input = fastJSON.JSON.Instance.ToJSON(dummyBeginGameRequest());
            _start = DateTime.Now;
            var foo = JsonConvert.DeserializeObject<Dictionary<string, object>>(input);
            //var foo = fastJSON.JSON.Instance.Parse(input) as Dictionary<string, object>;
            var a = foo["product_key"];
            var output = buildResponse(MesserSmashWeb.REQUEST_BEGIN_GAME, input);

            //--status request--
            //var statusInput = fastJSON.JSON.Instance.ToJSON(dummyStatusRequest());
            //var gameid = "f0a0a00a";
            //_start = DateTime.Now;
            //if (verifyGameId(gameid)) {
            //    var statusOutput = buildResponse("status", statusInput);
            //} else {
            //    var statusOutput = buildInvalidGameidResponse("status", gameid, statusInput);
            //}
            //Logger.flush();


            //--test load game--
            //using (var sr = new StreamReader("../../../../bin/debug/games/last_save.txt")) {
            //    var s = sr.ReadToEnd();
            //    var state = fastJSON.JSON.Instance.Parse(s);
            //    GameStates status = fastJSON.JSON.Instance.FillObject(new GameStates(), s) as GameStates;
                //var keyboardStates = state.KeyboardStates;
                //foreach (var keyb in keyboardStates) {
                //    if (keyb.GetPressedKeys().Length > 0) {
                //        var keys = keyb.GetPressedKeys();
                //    }
                //}
            //}
        }

        private static bool verifyGameId(string gameid) {
            return true;
        }

        private static Dictionary<string, object> dummyBeginGameRequest() {
            var d = new Dictionary<string, object> {
                {MesserSmashWeb.VERIFIED_LOGIN_SESSION, "abc0123"},
                {MesserSmashWeb.USER_NAME, "MesSer"},
                {MesserSmashWeb.USER_ID, "MesSer1024"},
                {MesserSmashWeb.PLAYER_STATE, 
                    new Dictionary<string, object> {
                        {"asdf", "123"},
                        {"foo", "bar"}
                    }
                }
            };

            return d;
        }

        private static Dictionary<string, object> dummyStatusRequest() {
            var d = new Dictionary<string, object> {
                {MesserSmashWeb.USER_NAME, "MesSer"},
                {MesserSmashWeb.USER_ID, "MesSer1024"},
                {MesserSmashWeb.LEVEL, 1},
                {MesserSmashWeb.KILLS, 138},
                {MesserSmashWeb.SCORE, 1258},
                {MesserSmashWeb.TOTAL_GAME_TIME, 18.897987},
                {MesserSmashWeb.TIME_MULTIPLIER, 1},
                {MesserSmashWeb.RANDOM_SEED, 0},
                {MesserSmashWeb.KEYBOARD_STATES, 
                    new ArrayList {
                        new ArrayList {"W","S"},
                        new ArrayList {},
                        new ArrayList {"W","S"},
                        new ArrayList {"W"},
                        new ArrayList {"W","A","D"}
                    }
                },
                {MesserSmashWeb.DELTA_TIME, 
                    new ArrayList {
                        0,
                        15,
                        16,
                        17,
                        18
                    }
                },
                {MesserSmashWeb.MOUSE_STATES, 
                    new ArrayList {
                        new ArrayList {},
                        new ArrayList {},
                        new ArrayList {"LMB"},
                        new ArrayList {},
                        new ArrayList {"LMB", "RMB"}
                    }
                }
            };

            return d;

        }

        private static string buildResponse(string id, string data) {
            int status_code = 0;
            Dictionary<String, object> result;
            switch (id) {
                case MesserSmashWeb.REQUEST_BEGIN_GAME: {
                        GameHandler.verifyDataStartGame(data, out status_code);
                        var guid = Guid.NewGuid().ToString();
                        result = new Dictionary<String, object> {
                            {MesserSmashWeb.STATUS_CODE, status_code},
                            {MesserSmashWeb.GAME_ID, status_code == 0 ? guid : ""}
                        };
                    }
                    break;
                case MesserSmashWeb.REQUEST_UPDATE_STATUS: {
                        GameHandler.verifyDataStatus(data, out status_code);
                        result = new Dictionary<string, object> {
                                {MesserSmashWeb.STATUS_CODE, status_code}
                            };
                    }
                    break;
                case MesserSmashWeb.REQUEST_END_GAME: {
                        GameHandler.verifyDataStatus(data, out status_code);
                        if (status_code == 0) {
                            var level = (int)GameHandler.readLevel();
                            result = new Dictionary<string, object> {
                                {MesserSmashWeb.STATUS_CODE, status_code},
                                {MesserSmashWeb.TOP_SCORES, generateHighscoreList(10, level)},
                                {MesserSmashWeb.USERS_HIGHSCORE_INFO, generateUserScore(level)}
                            };
                        } else {
                            result = new Dictionary<string, object> {
                                {MesserSmashWeb.STATUS_CODE, status_code}
                            };
                        }
                    }
                    break;
                default:
                    Logger.error("Unknown request id={0}, data={1}", id, data);
                    return "";
            }
            var output = JsonConvert.SerializeObject(result);
            Logger.info("Handled |{2}|-request in {0}ms \n\tinput:\t{3} \n\toutput:\t{1}", (DateTime.Now - _start).TotalMilliseconds, output, id, data);
            return output;
        }

        private static Dictionary<string, object> generateUserScore(int level) {
            int rank = 756;
            var d = new Dictionary<string, object> {
                {MesserSmashWeb.SCORE, GameHandler.readScore()},
                {MesserSmashWeb.KILLS, GameHandler.readKills()},
                {MesserSmashWeb.USER_NAME, GameHandler.readUserName()},
                {MesserSmashWeb.RANK, rank}
            };
            return d;
        }

        private static string buildInvalidGameidResponse(string request, string gameid, string data) {
            int invalid_gameid = -1;
            int invalid_data = -10;

            Dictionary<string, object> result;
            switch (request) {
                case MesserSmashWeb.REQUEST_UPDATE_STATUS: {
                        result = new Dictionary<string, object> {
                                {MesserSmashWeb.STATUS_CODE, invalid_gameid}
                            };
                    }
                    break;
                case MesserSmashWeb.REQUEST_END_GAME: {
                        int code;
                        GameHandler.verifyDataStatus(data, out code);
                        if (code == 0) {
                            int level = (int)GameHandler.readLevel();
                            var highscores = generateHighscoreList(10, level);

                            result = new Dictionary<string, object> {
                            {MesserSmashWeb.STATUS_CODE, invalid_gameid},
                            {MesserSmashWeb.TOP_SCORES, highscores}
                        };
                        } else {
                            result = new Dictionary<string, object> {
                            {MesserSmashWeb.STATUS_CODE, invalid_data}
                        };
                        }
                    }
                    break;
                default:
                    Logger.error("Unknown build error request id={0}, data={1}, gameid={2}", request, data, gameid);
                    return "";
            }
            var output = JsonConvert.SerializeObject(result);
            Logger.info("InvalidGameid |{2}|-request handled during {0}ms \n\tinput:\t{3} \n\toutput:\t{1}", (DateTime.Now - _start).TotalMilliseconds, output, request, data);
            return output;
        }

        private static ArrayList generateHighscoreList(int number, int level) {
            ArrayList output = new ArrayList();
            for (int i = 0; i < number; i++) {
                if(i == 0) {
                    output.Add( new Dictionary<string, object> {
                                {MesserSmashWeb.USER_NAME, "foobar1"},
                                {MesserSmashWeb.KILLS, 357},
                                {MesserSmashWeb.SCORE, 4269}
                            });
                } else {
                    output.Add( new Dictionary<string, object> {
                                {MesserSmashWeb.USER_NAME, "foobar" + i},
                                {MesserSmashWeb.KILLS, 350},
                                {MesserSmashWeb.SCORE, 4260}
                            });
                
                }
            }
            return output;
        }
    }
}
