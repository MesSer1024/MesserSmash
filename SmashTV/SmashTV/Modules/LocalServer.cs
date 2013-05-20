using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedSmashResources;
using System.IO.Compression;
using System.IO;
using System.Net;

namespace MesserSmash.Modules {
    class LocalServer {
        private string _url = "http://pawncraft.co.uk:8800/MesserSmash/";
        
        public LocalServer() {
        }

        public void send(StatusUpdate states) {
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

                string responseFromServer = reader.ReadToEnd();
                // Display the content.
                Console.WriteLine(responseFromServer);

            }
        }

        public StatusUpdate receive(byte[] data) {
            //var stringData = fastJSON.JSON.Instance.Parse(Unzip(data));
            StatusUpdate status = fastJSON.JSON.Instance.FillObject(new StatusUpdate(), Unzip(data)) as StatusUpdate;
            //StatusUpdate status = fastJSON.JSON.Instance.FillObject(new StatusUpdate(), stringData) as StatusUpdate;
            return status;
        }

        private void CopyTo(Stream src, Stream dest) {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0) {
                dest.Write(bytes, 0, cnt);
            }
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
    }
}
