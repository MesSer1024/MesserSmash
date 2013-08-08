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
        public delegate MesserWebResponse WebDelegate(string request, string rawData);
        private readonly WebDelegate _responderMethod;
        private string _url;
        private LocalServer _server;

        public LocalServer LocalServer {
            get { return _server; }
            set { _server = value; }
        }

        public MesserSmashWebServer(WebDelegate method, params string[] prefixes) {
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
                                
                                //browserRequest (from non-game-client)
                                if (request == null || request == "")
                                {
                                    request = httpRequest.Headers["browserRequest"];
                                    if (httpRequest != null && httpRequest.HasEntityBody)
                                    {
                                        var data = new byte[httpRequest.ContentLength64];
                                        using (System.IO.Stream body = httpRequest.InputStream)
                                        {
                                            body.Read(data, 0, data.Length);
                                        };
                                        var rawData = System.Text.ASCIIEncoding.ASCII.GetString(data);
                                        var foo = _responderMethod(request, rawData);
                                        responseString = foo.ServerResponse;
                                        rc = foo.ReturnCode.ToString();
                                        if (request == MesserSmashWeb.REQUEST_END_GAME)
                                        {
                                            Logger.debug("Incoming: request={1}, data=(stripped)", rawData, request);
                                            Logger.debug("Outgoing: request={1}, data={0}", responseString, request);
                                        }
                                        else
                                        {
                                            Logger.debug("Incoming: request={1}, data={0}", rawData, request);
                                            Logger.debug("Outgoing: request={1}, data={0}", responseString, request);
                                        }
                                    }
                                    else
                                    {
                                        rc = "1";
                                    }
                                }
                                else //request (from game-client)
                                {
                                    if (httpRequest != null && httpRequest.HasEntityBody)
                                    {
                                        var data = new byte[httpRequest.ContentLength64];
                                        using (System.IO.Stream body = httpRequest.InputStream)
                                        {
                                            body.Read(data, 0, data.Length);
                                        };
                                        var rawData = _server.unparse(data);
                                        var foo = _responderMethod(request, rawData);
                                        responseString = foo.ServerResponse;
                                        rc = foo.ReturnCode.ToString();
                                        if (request == MesserSmashWeb.REQUEST_END_GAME)
                                        {
                                            Logger.debug("Incoming: request={1}, data=(stripped)", rawData, request);
                                            Logger.debug("Outgoing: request={1}, data={0}", responseString, request);
                                        }
                                        else
                                        {
                                            Logger.debug("Incoming: request={1}, data={0}", rawData, request);
                                            Logger.debug("Outgoing: request={1}, data={0}", responseString, request);
                                        }
                                    }
                                    else
                                    {
                                        rc = "1";
                                    }
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
                                Logger.info("Handled request={0} in {1}ms user:{2}", request, (DateTime.Now - timestamp).TotalMilliseconds, ServerModel.UserName);
                                Console.WriteLine("Handled request={0} in {1}ms user:{2}", request, (DateTime.Now - timestamp).TotalMilliseconds, ServerModel.UserName);
                            }
                        }, _listener.GetContext());
                    }
                } catch { } // suppress any exceptions

            });
        }

        public void Stop() {
            _listener.Stop();
            _listener.Close();
            Logger.clean();
        }
    }
}
