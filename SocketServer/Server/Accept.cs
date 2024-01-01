using SocketServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Server
{
    public partial class Server
    {
        public void Accept(IAsyncResult asyncResult)
        {
            manualResetEvent.Set();
            Socket listener = (Socket)asyncResult.AsyncState, handler = listener!.EndAccept(asyncResult);

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
