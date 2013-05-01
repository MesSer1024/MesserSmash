using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MesserSmash.Modules;
using SharedSmashResources;
using System.Collections;
using Microsoft.Xna.Framework;

namespace MesserSmashWebServer {
    class MesserSmashWebMain {

        private class JsonDictionary : Dictionary<string, object> {}
        private static DateTime _start;
        static void Main(string[] args) {
            //--begin request--
            //var input = fastJSON.JSON.Instance.ToJSON(dummyBeginGameRequest());
            //_start = DateTime.Now;
            //var output = buildResponse("begin", input);
            //Logger.flush();

            //--status request--
            var statusInput = fastJSON.JSON.Instance.ToJSON(dummyStatusRequest());
            var gameid = "f0a0a00a";
            _start = DateTime.Now;
            if (verifyGameId(gameid)) {
                var statusOutput = buildResponse("status", statusInput);
            } else {
                var statusOutput = buildInvalidGameidResponse("status", gameid, statusInput);
            }
            Logger.flush();
        }

        private static bool verifyGameId(string gameid) {
            return true;
        }

        private static JsonDictionary dummyBeginGameRequest() {
            var d = new JsonDictionary {
                {SmashWebIdentifiers.PRODUCT_KEY, "abc0123"},
                {SmashWebIdentifiers.USER_NAME, "MesSer"},
                {SmashWebIdentifiers.USER_ID, "MesSer1024"},
                {SmashWebIdentifiers.PLAYER_STATE, 
                    new JsonDictionary {
                        {"asdf", "123"},
                        {"foo", "bar"}
                    }
                }
            };

            return d;
        }

        private static JsonDictionary dummyStatusRequest() {
            var d = new JsonDictionary {
                {SmashWebIdentifiers.USER_NAME, "MesSer"},
                {SmashWebIdentifiers.USER_ID, "MesSer1024"},
                {SmashWebIdentifiers.LEVEL, 1},
                {SmashWebIdentifiers.KILLS, 138},
                {SmashWebIdentifiers.SCORE, 1258},
                {SmashWebIdentifiers.TOTAL_GAME_TIME, 18.897987},
                {SmashWebIdentifiers.TIME_MULTIPLIER, 1},
                {SmashWebIdentifiers.RANDOM_SEED, 0},
                {SmashWebIdentifiers.KEYBOARD_STATES, 
                    new ArrayList {
                        new ArrayList {"W","S"},
                        new ArrayList {},
                        new ArrayList {"W","S"},
                        new ArrayList {"W"},
                        new ArrayList {"W","A","D"}
                    }
                },
                {SmashWebIdentifiers.DELTA_TIME, 
                    new ArrayList {
                        0,
                        15,
                        16,
                        17,
                        18
                    }
                },
                {SmashWebIdentifiers.MOUSE_STATES, 
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
                case SmashWebIdentifiers.REQUEST_BEGIN: {
                        GameHandler.verifyDataStartGame(data, out status_code);
                        var guid = Guid.NewGuid().ToString();
                        result = new Dictionary<String, object> {
                            {SmashWebIdentifiers.STATUS_CODE, status_code},
                            {SmashWebIdentifiers.GAME_ID, status_code == 0 ? guid : ""}
                        };
                    }
                    break;
                case SmashWebIdentifiers.REQUEST_STATUS: {
                        GameHandler.verifyDataStatus(data, out status_code);
                        result = new Dictionary<string, object> {
                                {SmashWebIdentifiers.STATUS_CODE, status_code}
                            };
                    }
                    break;
                case SmashWebIdentifiers.REQUEST_FINAL: {
                        GameHandler.verifyDataStatus(data, out status_code);
                        if (status_code == 0) {
                            var level = (int)GameHandler.readLevel();
                            result = new Dictionary<string, object> {
                                {SmashWebIdentifiers.STATUS_CODE, status_code},
                                {SmashWebIdentifiers.TOP_SCORES, generateHighscoreList(10, level)},
                                {SmashWebIdentifiers.USERS_HIGHSCORE_INFO, generateUserScore(level)}
                            };
                        } else {
                            result = new Dictionary<string, object> {
                                {SmashWebIdentifiers.STATUS_CODE, status_code}
                            };
                        }
                    }
                    break;
                default:
                    Logger.error("Unknown request id={0}, data={1}", id, data);
                    return "";
            }
            var output = fastJSON.JSON.Instance.ToJSON(result);
            Logger.info("Handled |{2}|-request in {0}ms \n\tinput:\t{3} \n\toutput:\t{1}", (DateTime.Now - _start).TotalMilliseconds, output, id, data);
            return output;
        }

        private static Dictionary<string, object> generateUserScore(int level) {
            int rank = 756;
            var d = new Dictionary<string, object> {
                {SmashWebIdentifiers.SCORE, GameHandler.readScore()},
                {SmashWebIdentifiers.KILLS, GameHandler.readKills()},
                {SmashWebIdentifiers.USER_NAME, GameHandler.readUserName()},
                {SmashWebIdentifiers.RANK, rank}
            };
            return d;
        }

        private static string buildInvalidGameidResponse(string request, string gameid, string data) {
            int invalid_gameid = -1;
            int invalid_data = -10;

            Dictionary<string, object> result;
            switch (request) {
                case SmashWebIdentifiers.REQUEST_STATUS: {
                        result = new Dictionary<string, object> {
                                {SmashWebIdentifiers.STATUS_CODE, invalid_gameid}
                            };
                    }
                    break;
                case SmashWebIdentifiers.REQUEST_FINAL: {
                        int code;
                        GameHandler.verifyDataStatus(data, out code);
                        if (code == 0) {
                            int level = (int)GameHandler.readLevel();
                            var highscores = generateHighscoreList(10, level);

                            result = new Dictionary<string, object> {
                            {SmashWebIdentifiers.STATUS_CODE, invalid_gameid},
                            {SmashWebIdentifiers.TOP_SCORES, highscores}
                        };
                        } else {
                            result = new Dictionary<string, object> {
                            {SmashWebIdentifiers.STATUS_CODE, invalid_data}
                        };
                        }
                    }
                    break;
                default:
                    Logger.error("Unknown build error request id={0}, data={1}, gameid={2}", request, data, gameid);
                    return "";
            }
            var output = fastJSON.JSON.Instance.ToJSON(result);
            Logger.info("InvalidGameid |{2}|-request handled during {0}ms \n\tinput:\t{3} \n\toutput:\t{1}", (DateTime.Now - _start).TotalMilliseconds, output, request, data);
            return output;
        }

        private static ArrayList generateHighscoreList(int number, int level) {
            ArrayList output = new ArrayList();
            for (int i = 0; i < number; i++) {
                if(i == 0) {
                    output.Add( new Dictionary<string, object> {
                                {SmashWebIdentifiers.USER_NAME, "foobar1"},
                                {SmashWebIdentifiers.KILLS, 357},
                                {SmashWebIdentifiers.SCORE, 4269}
                            });
                } else {
                    output.Add( new Dictionary<string, object> {
                                {SmashWebIdentifiers.USER_NAME, "foobar" + i},
                                {SmashWebIdentifiers.KILLS, 350},
                                {SmashWebIdentifiers.SCORE, 4260}
                            });
                
                }
            }
            return output;
        }
    }
}
