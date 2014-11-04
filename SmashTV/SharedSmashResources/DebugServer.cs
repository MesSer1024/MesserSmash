using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SharedSmashResources {
    public class DebugServer {

        public DebugServer() {
            var listener = new TcpListener(IPAddress.Loopback, 6677);
            listener.Start();
            while (true) {
                using (var client = listener.AcceptTcpClient())
                using (var stream = client.GetStream()) {
                    var headers = new Dictionary<string, string>();
                    string line = string.Empty;
                    while ((line = ReadLine(stream)) != string.Empty) {
                        var tokens = line.Split(new char[] { ':' }, 2);
                        if (!string.IsNullOrWhiteSpace(line) && tokens.Length > 1) {
                            headers[tokens[0]] = tokens[1].Trim();
                        }
                    }

                    var key = new byte[8];
                    //stream.Read(key, 0, key.Length);

                    var key1 = headers["Sec-WebSocket-Key"];
                    var key2 = headers["Sec-WebSocket-Key2"];

                    var numbersKey1 = Convert.ToInt64(string.Join(null, Regex.Split(key1, "[^\\d]")));
                    var numbersKey2 = Convert.ToInt64(string.Join(null, Regex.Split(key2, "[^\\d]")));
                    var numberSpaces1 = CountSpaces(key1);
                    var numberSpaces2 = CountSpaces(key2);

                    var part1 = (int)(numbersKey1 / numberSpaces1);
                    var part2 = (int)(numbersKey2 / numberSpaces2);

                    var result = new List<byte>();
                    result.AddRange(GetBigEndianBytes(part1));
                    result.AddRange(GetBigEndianBytes(part2));
                    result.AddRange(key);

                    var response =
                        "HTTP/1.1 101 WebSocket Protocol Handshake" + Environment.NewLine +
                        "Upgrade: WebSocket" + Environment.NewLine +
                        "Connection: Upgrade" + Environment.NewLine +
                        "Sec-WebSocket-Origin: " + headers["Origin"] + Environment.NewLine +
                        "Sec-WebSocket-Location: ws://localhost:6677/websession" + Environment.NewLine +
                        Environment.NewLine;

                    var bufferedResponse = Encoding.UTF8.GetBytes(response);
                    stream.Write(bufferedResponse, 0, bufferedResponse.Length);
                    using (var md5 = MD5.Create()) {
                        var handshake = md5.ComputeHash(result.ToArray());
                        stream.Write(handshake, 0, handshake.Length);
                    }
                }
            }
        }

        public DebugServer(int foo)
        {

        }

        static int CountSpaces(string key) {
            return key.Length - key.Replace(" ", string.Empty).Length;
        }

        static string ReadLine(Stream stream) {
            var sb = new StringBuilder();
            var buffer = new List<byte>();
            while (true) {
                buffer.Add((byte)stream.ReadByte());
                var line = Encoding.ASCII.GetString(buffer.ToArray());
                if (line.EndsWith(Environment.NewLine)) {
                    return line.Substring(0, line.Length - 2);
                }
            }
        }

        static byte[] GetBigEndianBytes(int value) {
            var bytes = 4;
            var buffer = new byte[bytes];
            int num = bytes - 1;
            for (int i = 0; i < bytes; i++) {
                buffer[num - i] = (byte)(value & 0xffL);
                value = value >> 8;
            }
            return buffer;
        }

        //////////////////////////////////// ATTEMPT 2 //////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        ///
        ///
        static Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        static private string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

        static void Main(string[] args)
        {
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 8080));
            serverSocket.Listen(128);
            serverSocket.BeginAccept(null, 0, OnAccept, null);
            Console.Read();
        }

        private static void OnAccept(IAsyncResult result)
        {
            byte[] buffer = new byte[1024];
            try
            {
                Socket client = null;
                string headerResponse = "";
                if (serverSocket != null && serverSocket.IsBound)
                {
                    client = serverSocket.EndAccept(result);
                    var i = client.Receive(buffer);
                    headerResponse = (System.Text.Encoding.UTF8.GetString(buffer)).Substring(0, i);
                    // write received data to the console
                    Console.WriteLine(headerResponse);

                }
                if (client != null)
                {
                    /* Handshaking and managing ClientSocket */

                    var key = headerResponse.Replace("ey:", "`")
                              .Split('`')[1]                     // dGhlIHNhbXBsZSBub25jZQ== \r\n .......
                              .Replace("\r", "").Split('\n')[0]  // dGhlIHNhbXBsZSBub25jZQ==
                              .Trim();

                    // key should now equal dGhlIHNhbXBsZSBub25jZQ==
                    var test1 = AcceptKey(ref key);

                    var newLine = "\r\n";

                    var response = "HTTP/1.1 101 Switching Protocols" + newLine
                         + "Upgrade: websocket" + newLine
                         + "Connection: Upgrade" + newLine
                         + "Sec-WebSocket-Accept: " + test1 + newLine + newLine
                        //+ "Sec-WebSocket-Protocol: chat, superchat" + newLine
                        //+ "Sec-WebSocket-Version: 13" + newLine
                         ;

                    // which one should I use? none of them fires the onopen method
                    client.Send(System.Text.Encoding.UTF8.GetBytes(response));

                    var i = client.Receive(buffer); // wait for client to send a message

                    // once the message is received decode it in different formats
                    Console.WriteLine(Convert.ToBase64String(buffer).Substring(0, i));

                    Console.WriteLine("\n\nPress enter to send data to client");
                    Console.Read();

                    var subA = SubArray<byte>(buffer, 0, i);
                    client.Send(subA);
                    Thread.Sleep(10000);//wait for message to be send


                }
            }
            catch (SocketException exception)
            {
                throw exception;
            }
            finally
            {
                if (serverSocket != null && serverSocket.IsBound)
                {
                    serverSocket.BeginAccept(null, 0, OnAccept, null);
                }
            }
        }

        public static T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private static string AcceptKey(ref string key)
        {
            string longKey = key + guid;
            byte[] hashBytes = ComputeHash(longKey);
            return Convert.ToBase64String(hashBytes);
        }

        static SHA1 sha1 = SHA1CryptoServiceProvider.Create();
        private static byte[] ComputeHash(string str)
        {
            return sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
        }
    }
}