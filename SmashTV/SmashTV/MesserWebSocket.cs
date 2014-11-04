using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using SuperWebSocket;
using MesserSmash.Commands;

namespace SharedSmashResources {
    public class MesserWebSocket {
        WebSocketServer _server;

        public MesserWebSocket() {
            _server = new WebSocketServer();
            _server.Setup(6677);
            _server.NewSessionConnected += new SuperSocket.SocketBase.SessionHandler<WebSocketSession>(_server_NewSessionConnected);
            _server.NewMessageReceived += new SuperSocket.SocketBase.SessionHandler<WebSocketSession, string>(_server_NewMessageReceived);
            _server.NewDataReceived += new SuperSocket.SocketBase.SessionHandler<WebSocketSession, byte[]>(_server_NewDataReceived);
            _server.Start();
        }

        void _server_NewDataReceived(WebSocketSession session, byte[] value) {
            session.Send("New data received!");
        }

        void _server_NewMessageReceived(WebSocketSession session, string value) {
            session.Send("Message: " + value);
            new RemoteLevelCommand(value).execute();
        }

        void _server_NewSessionConnected(WebSocketSession session) {
            session.Send("Hello from C#!");
        }
    }
}