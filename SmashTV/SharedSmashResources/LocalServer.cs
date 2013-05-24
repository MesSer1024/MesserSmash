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
        private string _url;

        public LocalServer(string url) {
            // TODO: Complete member initialization
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

        public bool send(StatusUpdate states) {
            try {
                byte[] data = Zip(fastJSON.JSON.Instance.ToJSON(states));
                var req = System.Net.WebRequest.Create(_url);
                req.ContentType = "application/x-www-form-urlencoded";
                req.Method = "POST";
                req.ContentLength = data.Length;
                using (Stream dataStream = req.GetRequestStream()) {
                    dataStream.Write(data, 0, data.Length);
                }


                // Get the response.
                using (WebResponse response = req.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream)) {
                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                    // Get the stream containing content returned by the server.

                    var serverResponse = reader.ReadToEnd();
                    // Display the content.
                    Console.WriteLine(serverResponse);
                }
            } catch (Exception e) {
                Logger.error("Error when communicating with server:\n {0} \n\n{1}", e.ToString(), e.StackTrace);
                return false;
            }
            return true;
        }

        public StatusUpdate receive(byte[] data) {
            StatusUpdate status = null;
            var unzipped = Unzip(data);
            status = fastJSON.JSON.Instance.FillObject(new StatusUpdate(), unzipped) as StatusUpdate;
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
                    //gs.CopyTo(mso);
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
    }
}
