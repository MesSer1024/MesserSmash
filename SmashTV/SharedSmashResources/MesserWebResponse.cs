using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedSmashResources {
    public enum ReturnCodes {
        OK = 0,
        TIMEOUT = -1,
        GENERAL_ERROR = -666,
        USER_EXISTS = 1,
    }

    public class MesserWebResponse {
        //return codes

        //
        public int ReturnCode { get; private set; }
        public string ServerResponse { get; private set; }
        public string ClientRequest { get; private set; }

        public MesserWebResponse(int rc, string data, string request) {
            ReturnCode = rc;
            ServerResponse = data;
            ClientRequest = request;
        }

        public MesserWebResponse(ReturnCodes rc, string data, string request) : this((int)rc, data, request) {
        }

        public static MesserWebResponse TimeoutResponse(string request) {
            return new MesserWebResponse(ReturnCodes.TIMEOUT, "", request);
        }

        public static MesserWebResponse GeneralError(string request) {
            return new MesserWebResponse(ReturnCodes.GENERAL_ERROR, "", request);
        }
    }
}
