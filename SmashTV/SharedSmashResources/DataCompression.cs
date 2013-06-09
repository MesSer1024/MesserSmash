using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace SharedSmashResources {
    public class DataCompression {
        private Encoding encoding = Encoding.UTF8;
        public byte[] Zip(string str) {
             var bytes = encoding.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(mso, CompressionMode.Compress)) {
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }

        public string UnzipFromFile(FileInfo file) {
            using (var sr = new StreamReader(file.FullName, Encoding.UTF8)) {
                var s = sr.ReadToEnd();
                var data = Unzip(s);
                return data;
            }
        }

        public byte[] Unmarshal(string s) {
            return encoding.GetBytes(s);
        }

        public string Unzip(string s) {
            var bytes = encoding.GetBytes(s);
            return Unzip(bytes);
        }

        public string Unzip(byte[] bytes) {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress)) {
                    CopyTo(gs, mso);
                }

                return encoding.GetString(mso.ToArray());
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
