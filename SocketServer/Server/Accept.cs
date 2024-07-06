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
        /// <summary>
        /// `Accept` - accepts connection from client towards network ( tcp/ip )
        /// </summary>
        /// <param name="asyncResult"></param>
        public void Accept(IAsyncResult asyncResult)
        {
            manualResetEvent.Set();
            Socket listener = (Socket)asyncResult!.AsyncState!, handler = listener!.EndAccept(asyncResult);

            var state = new State();
            state.Listener = handler;

            handler.BeginReceive(
                state.buffer,
                offset:0,
                size:1024,
                socketFlags:0,
                callback:new AsyncCallback(Receive),
                state);
        }
    }
}
