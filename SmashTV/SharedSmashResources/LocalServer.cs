using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO.Compression;
using System.IO;
using System.Net;
using MesserSmash.Modules;

namespace SharedSmashResources {
    public class LocalServer {
        private class Headers : List<KeyValuePair<string, string>> { };
        private string _url;

        public LocalServer(string url) {
            _url = url;
        }

        public void log(byte[] data) {
            try
            {
                var unzipped = Unzip(data);
                Logger.error("error unzipped data={0}", unzipped);
            }
            catch (System.Exception e)
            {
                Logger.error("error parsing data:{0}", e.ToString());
            }
        }

        public string readData(byte[] data) {
            return Unzip(data);
        }

        private WebRequest postWebRequestAndGetHandle(string url, string request, byte[] data) {
            var req = System.Net.WebRequest.Create(url);
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
            req.Headers.Add("request", request);
            req.ContentLength = data.Length;
            try {
                using (Stream dataStream = req.GetRequestStream()) {
                    dataStream.Write(data, 0, data.Length);
                }
            } catch (Exception e) {
                Logger.error("Exception in postWebRequestAndGetHandle: {0}", e.ToString());
            }
            return req;
        }

        private string postWebRequestAndGetResponse(string url, string request, byte[] data, WebHeaderCollection additionalHeaders = null) {
            try {
                var req = postWebRequestAndGetHandle(url, request, data);
                if (additionalHeaders != null) {
                    req.Headers.Add(additionalHeaders);
                }
                using (WebResponse response = req.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream)) {
                    // Get the stream containing content returned by the server.
                    var rc = req.Headers["rc"];
                    var serverResponse = reader.ReadToEnd();
                    return serverResponse;
                }
            }
            catch (System.Exception ex)
            {
                Logger.error("Error communicating with server: {0}", ex.ToString());
            }
            return "";
        }

        public bool sendGameState(GameStates states) {
            try {
                byte[] data = Zip(fastJSON.JSON.Instance.ToJSON(states));
                var response = postWebRequestAndGetResponse(_url, SmashWebIdentifiers.REQUEST_SAVE_GAME, data);
                Logger.info(String.Format("sendGameState ServerResponse={0}", response));
            } catch (Exception e) {
                Logger.error("Error when communicating with server:\n {0} \n\n{1}", e.ToString(), e.StackTrace);
                return false;
            }
            return true;
        }

        public GameStates buildGameState(byte[] data) {
            GameStates status = null;
            var unzipped = Unzip(data);
            status = fastJSON.JSON.Instance.FillObject(new GameStates(), unzipped) as GameStates;
            return status;
        }

        private byte[] Zip(string str) {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(mso, CompressionMode.Compress)) {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        private string Unzip(byte[] bytes) {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress)) {
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }

        private void CopyTo(Stream src, Stream dest) {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                dest.Write(bytes, 0, cnt);
            }
        }

        public void requestHighscores( Action<int,string> rsp, int level) {
            var response = postWebRequestAndGetResponse(_url, SmashWebIdentifiers.REQUEST_GET_HIGHSCORE_ON_LEVEL, Zip(String.Format("Level={0}", level)));
            rsp.Invoke(0, response);
            //var rsp = postWebRequestAndGetResponse()
        }
    }
}
