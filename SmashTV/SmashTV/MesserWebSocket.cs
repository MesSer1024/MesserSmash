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
using MesserSmash.Arenas;
using Newtonsoft.Json;

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
            var parts = value.Split('|');
            if (parts.Length == 2) {
                var id = parts[0];
                var data = parts[1];
                switch (id) {
                    case "level":
                        new RemoteLevelCommand(data).execute();
                        break;
                    case "save":
                        var levels = LevelBuilder.generateLevel(data);
                        LevelBuilder.SaveLevelData(levels[0]); //#TODO#
                        break;
                    case "load":
                        var level = LevelBuilder.GetLevelData(int.Parse(data));
                        session.Send("load|" + JsonConvert.SerializeObject(level));
                        break;
                }
            } else {
                throw new Exception("Expected id|data in message");
                //new RemoteLevelCommand(value).execute();
            }
        }

        void _server_NewSessionConnected(WebSocketSession session) {
            session.Send("Hello from C#!");
        }
    }
}