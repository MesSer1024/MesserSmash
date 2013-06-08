using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using MesserSmash.Modules;
using SharedSmashResources;

namespace MesserSmashWebServer {
    class MesserSmashWebServer {
        private readonly HttpListener _listener = new HttpListener();
        private readonly Func<string, string, string> _responderMethod;
        private string _url;
        private LocalServer _server;

        public LocalServer LocalServer {
            get { return _server; }
            set { _server = value; }
        }

        public MesserSmashWebServer(string[] prefixes, Func<string, string, string> method) {
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
            _url = prefixes[0];
            _server = new LocalServer(_url);

            foreach (string s in prefixes)
                _listener.Prefixes.Add(s);

            _responderMethod = method;
            _listener.Start();
        }

        public MesserSmashWebServer(Func<string, string, string> method, params string[] prefixes)
            : this(prefixes, method) { }

        public void Run() {
            ThreadPool.QueueUserWorkItem((o) => {
                Console.WriteLine("Webserver running...");
                try {
                    while (_listener.IsListening) {
                        Console.WriteLine("<-Got data");
                        ThreadPool.QueueUserWorkItem((c) => {
                            var ctx = c as HttpListenerContext;
                            var timestamp = DateTime.Now;
                            string request = "";
                            try {
                                var httpRequest = ctx.Request;
                                request = httpRequest.Headers["request"];
                                string responseString = null;
                                string rc = "0";
                                if (httpRequest != null && httpRequest.HasEntityBody) {
                                    var data = new byte[httpRequest.ContentLength64];
                                    using (System.IO.Stream body = httpRequest.InputStream) {
                                        body.Read(data, 0, data.Length);
                                    };
                                    var rawData = _server.unparse(data);
                                    responseString = _responderMethod(request, rawData);
                                } else {
                                    rc = "1";
                                }

                                ctx.Response.Headers.Add("request", request);
                                ctx.Response.Headers.Add("rc", rc);
                                //byte[] buf = _server.parse(responseString);
                                byte[] buf = Encoding.UTF8.GetBytes(responseString);
                                ctx.Response.ContentLength64 = buf.Length;
                                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                            } catch {
                                Console.WriteLine("->Error handling request");
                            } // suppress any exceptions
                            finally {
                                // always close the stream
                                ctx.Response.OutputStream.Close();
                                Logger.info("Handled request={0} in {1}ms", request, (DateTime.Now - timestamp).TotalMilliseconds);
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
}
