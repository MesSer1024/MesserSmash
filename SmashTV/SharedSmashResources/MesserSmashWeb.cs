﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace SharedSmashResources {
    public static class MesserSmashWeb {

        public static Dictionary<string, object> toObject(string s = null) {
            if (s == null || s == "") { return new Dictionary<string, object>(); }
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(s);
        }

        //game client
        public const string REQUEST_BEGIN_GAME = "beginGame";
        public const string REQUEST_CONTINUE_GAME = "continueGame";
        public const string REQUEST_UPDATE_STATUS = "status";
        public const string REQUEST_GET_HIGHSCORE_ON_LEVEL = "getHighscoreOnLevel";
        public const string REQUEST_GET_HIGHSCORE_FOR_ROUND = "getHighscoreForRound";
        public const string REQUEST_END_GAME = "final";

        //game launcher 
        public const string LAUNCHER_LOGIN = "login_request";
        public const string LAUNCHER_QUERY_VERSION = "version_check";
        public const string LAUNCHER_CREATE_USER = "create_user";
        public const string LAUNCHER_PASSWORD = "password";

        //misc
        public const string SCORE = "score";
        public const string GAME_VERSION = "game_version";
        public const string KILLS = "kills";
        public const string LEVEL = "level";
        public const string ROUND_ID = "round_id"; //used as a way to know what round a specific range of levels belong to, for instance level 1-10 = round0 etc...
        public const string VERIFIED_LOGIN_SESSION = "product_key";
        public const string USER_NAME = "user_name";
        public const string USER_ID = "user_id";
        public const string PLAYER_STATE = "player_state";
        public const string STATUS_CODE = "status_code";
        public const string GAME_ID = "game_id";
        public const string SESSION_ID = "session_id";
        public const string ENERGY = "energy";
        public const string TOTAL_GAME_TIME = "gametime";
        public const string TIME_MULTIPLIER = "multiplier";
        public const string RANDOM_SEED = "seed";
        public const string GAME_STATE = "game_state";
        public const string KEYBOARD_STATES = "keyboard_states";
        public const string MOUSE_STATES = "mouse_states";
        public const string DELTA_TIME = "delta_times";
        public const string TOP_SCORES = "top_scores";
        public const string RANK = "highscore_rank";
        public const string USERS_HIGHSCORE_INFO = "users_score";
        public const string GAME_VERSION_LATEST_URL = "file_url";
        public const string ENVIRONMENT_LAUNCHED_FROM_LAUNCHER = "messersmash_launched_from_launcher";
        public const string XNA_REDIST_URL = "xna_redist_url";

    }
}
