using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Server
{
    internal class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Sessions.Broadcast($"Server received message: {e.Data}");
        }

        protected override void OnOpen()
        {
            Sessions.Broadcast("Logged in!!");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Sessions.Broadcast("Server logged out!");
        }
    }
}
