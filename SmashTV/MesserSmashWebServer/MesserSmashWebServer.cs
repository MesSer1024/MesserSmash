using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace MesserSmashWebServer {
    class MesserSmashWebServer {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<HttpListenerRequest, string> _responderMethod;

        public MesserSmashWebServer(string[] prefixes, Func<HttpListenerRequest, string> method) {
            if (!HttpListener.IsSupported)
                throw new NotSupportedException(
                    "Needs Windows XP SP2, Server 2003 or later.");

            // URI prefixes are required, for example 
            // "http://localhost:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // A responder method is required
            if (method == null)
                throw new ArgumentException("method");

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _responderMethod = method;
            _listener.Start();
        }

        public MesserSmashWebServer(Func<HttpListenerRequest, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run() {
            ThreadPool.QueueUserWorkItem((o) => {
                Console.WriteLine("Webserver running...");
                try {
                    while (_listener.IsListening) {
                        Console.WriteLine("Got data->");
                        ThreadPool.QueueUserWorkItem((c) => {
                            var ctx = c as HttpListenerContext;
                            try {
                                string rstr = _responderMethod(ctx.Request);
                                byte[] buf = Encoding.UTF8.GetBytes(rstr);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            } catch { } // suppress any exceptions
                            finally {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                            }
                        }, _listener.GetContext());
                    }
                } catch { } // suppress any exceptions
            });
        }

        public void Stop() {
            _listener.Stop();
            _listener.Close();
        }
    }

    //The constructor which make the TcpListener start listening on th
    //given port. It also calls a Thread on the method StartListen(). 
    /*public MesserSmashWebServer() {
        try {
            //start listing on the given port
            myListener = new TcpListener(Dns.GetHostAddresses("http://pawncraft.co.uk:8800/MesserSmash/")[0], port);
            myListener.Start();
            Console.WriteLine("Web Server Running... Press ^C to Stop...");

            //start the thread which calls the method 'StartListen'
            Thread th = new Thread(new ThreadStart(StartListen));
            th.Start();

        } catch (Exception e) {
            Console.WriteLine("An Exception Occurred while Listening :"
                               + e.ToString());
        }
    }

    public void StartListen() {

        int iStartPos = 0;
        String sRequest;
        String sDirName;
        String sRequestedFile;
        String sErrorMessage;
        String sLocalDir;
        String sMyWebServerRoot = "C:\\MyWebServerRoot\\";
        String sPhysicalFilePath = "";
        String sFormattedMessage = "";
        String sResponse = "";


        while (true) {
            //Accept a new connection
            Socket mySocket = myListener.AcceptSocket();

            Console.WriteLine("Socket Type " + mySocket.SocketType);
            if (mySocket.Connected) {
                Console.WriteLine("\nClient Connected!!\n==================\n CLient IP {0}\n", mySocket.RemoteEndPoint);


                //make a byte array and receive data from the client 
                Byte[] bReceive = new Byte[1024];
                int i = mySocket.Receive(bReceive, bReceive.Length, 0);


                //Convert Byte to String
                string sBuffer = Encoding.ASCII.GetString(bReceive);


                //At present we will only deal with GET type
                if (sBuffer.Substring(0, 3) != "GET") {
                    Console.WriteLine("Only Get Method is supported..");
                    mySocket.Close();
                    return;
                }


                // Look for HTTP request
                iStartPos = sBuffer.IndexOf("HTTP", 1);


                // Get the HTTP text and version e.g. it will return "HTTP/1.1"
                string sHttpVersion = sBuffer.Substring(iStartPos, 8);


                // Extract the Requested Type and Requested file/directory
                sRequest = sBuffer.Substring(0, iStartPos - 1);


                //Replace backslash with Forward Slash, if Any
                sRequest.Replace("\\", "/");


                //If file name is not supplied add forward slash to indicate 
                //that it is a directory and then we will look for the 
                //default file name..
                if ((sRequest.IndexOf(".") < 1) && (!sRequest.EndsWith("/"))) {
                    sRequest = sRequest + "/";
                }
                //Extract the requested file name
                iStartPos = sRequest.LastIndexOf("/") + 1;
                sRequestedFile = sRequest.Substring(iStartPos);


                //Extract The directory Name
                sDirName = sRequest.Substring(sRequest.IndexOf("/"),
                           sRequest.LastIndexOf("/") - 3);
            }
        }
    }*/
}
