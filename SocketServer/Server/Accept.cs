using SocketServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Server
{
    using static Security.Utils.CustomLogger;
    public partial class Server
    {
        public void Accept(IAsyncResult asyncResult)
        {
            _semaphore.Release();

            Socket listener = (Socket)asyncResult.AsyncState!, handler = listener!.EndAccept(asyncResult);
            IPEndPoint? clientIp = handler.RemoteEndPoint as IPEndPoint;

            Logger.Information(nameof(Accept),$"Client with IP-Address `{clientIp?.Address.ToString()}` connected.");
            
            var state = new State();
            state.Listener = handler;
            handler.BeginReceive(
                state.buffer,
                0,
                1024,
                0,
                new AsyncCallback(Receive),
                state);
        }
    }
}
