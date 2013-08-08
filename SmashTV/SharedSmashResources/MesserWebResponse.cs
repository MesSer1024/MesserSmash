using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedSmashResources {
    public class MesserWebResponse {
        //return codes
        public const int RC_OK = 0;
        public const int RC_TIMEOUT = -1;
        public const int RC_GENERAL_ERROR = -666;

        //
        public int ReturnCode { get; private set; }
        public string ServerResponse { get; private set; }
        public string ClientRequest { get; private set; }

        public MesserWebResponse(int rc, string data, string request) {
            ReturnCode = rc;
            ServerResponse = data;
            ClientRequest = request;
        }

        public static MesserWebResponse TimeoutResponse(string request) {
            return new MesserWebResponse(RC_TIMEOUT, "", request);
        }

        public static MesserWebResponse InvalidResponse(string request) {
            return new MesserWebResponse(RC_GENERAL_ERROR, "", request);
        }
    }
}
