﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO.Compression;
using System.IO;
using System.Net;
using MesserSmash.Modules;
using System.Threading;
using Newtonsoft.Json;

namespace SharedSmashResources {
    public class LocalServer {
        private class Headers : List<KeyValuePair<string, string>> { };
        private DataCompression _compression;
        private string _url;

        public LocalServer(string url) {
            _compression = new DataCompression();
            _url = url;
        }

        public string unparse(byte[] data) {
            return _compression.Unzip(data);
        }

        public byte[] parse(string data) {
            return _compression.Zip(data);
        }

        public void log(byte[] data) {
            try {
                var unzipped = _compression.Unzip(data);
                Logger.error("error unzipped data={0}", unzipped);
            } catch (System.Exception e) {
                Logger.error("error parsing data:{0}", e.ToString());
            }
        }

        private MesserWebResponse postWebRequestAndGetResponse(string url, string request, byte[] data, int timeoutMs = 0, WebHeaderCollection additionalHeaders = null) {
            try {
                var req = System.Net.WebRequest.Create(url);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                req.Headers.Add("request", request);

                if (additionalHeaders != null) {
                    req.Headers.Add(additionalHeaders);
                }
                if (timeoutMs != 0) {
                    req.Timeout = timeoutMs;
                }

                req.ContentLength = data.Length;
                using (Stream dataStream = req.GetRequestStream()) {
                    dataStream.Write(data, 0, data.Length);
                }
                using (WebResponse response = req.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream)) {
                    // Get the stream containing content returned by the server.
                    var rc = int.Parse(response.Headers["rc"]);
                    var s = reader.ReadToEnd();
                    return new MesserWebResponse(rc, s, request);
                }
            } catch (System.Exception ex) {
                Logger.error("Error communicating with server: {0}", ex.ToString());
            }

            return MesserWebResponse.TimeoutResponse(request);
        }

        private byte[] GetBytes(string str) {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public GameStates handleSaveGame(string data) {
            GameStates status = null;
            status = JsonConvert.DeserializeObject<GameStates>(data);
            return status;
        }

        public bool requestSaveGame(GameStates states) {
            try {
                byte[] data = _compression.Zip(JsonConvert.SerializeObject(states));
                var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.REQUEST_END_GAME, data);
                Logger.info(String.Format("sendGameState ServerResponse={0}", response));
            } catch (Exception e) {
                Logger.error("Error when communicating with server:\n {0} \n\n{1}", e.ToString(), e.StackTrace);
                return false;
            }
            return true;
        }

        public void requestHighscores( Action<int,string> cb, Dictionary<string, object> data) {
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.REQUEST_GET_HIGHSCORE_ON_LEVEL, _compression.Zip(JsonConvert.SerializeObject(data)));
            cb.Invoke(response.ReturnCode, response.ServerResponse);
        }

        public void requestRoundHighscores(Action<int, string> cb, Dictionary<string, object> data) {
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.REQUEST_GET_HIGHSCORE_FOR_ROUND, _compression.Zip(JsonConvert.SerializeObject(data)));
            cb.Invoke(response.ReturnCode, response.ServerResponse);
        }

        public void requestBeginGame(Action<int, string> cb, Dictionary<string, object> data) {
            Logger.info("->requestBeginGame");
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.REQUEST_BEGIN_GAME, _compression.Zip(JsonConvert.SerializeObject(data)), 5000);
            cb.Invoke(response.ReturnCode, response.ServerResponse);
            Logger.info("<-requestBeginGame");
        }

        public void requestContinueGame(Action<int, string> cb, Dictionary<string, object> data) {
            Logger.info("->requestBeginGame");
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.REQUEST_CONTINUE_GAME, _compression.Zip(JsonConvert.SerializeObject(data)), 5000);
            cb.Invoke(response.ReturnCode, response.ServerResponse);
            Logger.info("<-requestBeginGame");
        }

        public void launcherLogin(Action<int, string> cb, Dictionary<string, object> data) {
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.LAUNCHER_LOGIN, _compression.Zip(JsonConvert.SerializeObject(data)), 5000);
            cb.Invoke(response.ReturnCode, response.ServerResponse);
        }

        public void launcherCreateUser(Action<int, string> cb, Dictionary<string, object> data) {
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.LAUNCHER_CREATE_USER, _compression.Zip(JsonConvert.SerializeObject(data)), 5000);
            cb.Invoke(response.ReturnCode, response.ServerResponse);
        }

        public void launcherCheckVersion(Action<int, string> cb, Dictionary<string, object> data) {
            var response = postWebRequestAndGetResponse(_url, MesserSmashWeb.LAUNCHER_QUERY_VERSION, _compression.Zip(JsonConvert.SerializeObject(data)), 5000);
            cb.Invoke(response.ReturnCode, response.ServerResponse);
        }
    }
}
