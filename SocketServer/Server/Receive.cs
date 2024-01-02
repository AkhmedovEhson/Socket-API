using Serilog;
using Serilog.Core;
using SocketServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SocketServer.Server
{
    public partial class Server
    {
        public void Receive(IAsyncResult? result)
        {
            State data = (State)result.AsyncState;

            try
            {
                int read = data.Listener.EndReceive(result);

                if (read > 0)
                {
                    string message = Convert.ToBase64String(data.buffer, 0, read);
                    Log.Information($"Received message: encrypted -> '{message}' <-> decrypted '{hashing.Decryption(message)}'");

                }

                data.Listener.BeginReceive(data.buffer,
                       0,
                       1024,
                       0,
                       new AsyncCallback(Receive), data);

            }
            catch(Exception e)
            {
                Log.Error(e.Message);
            }

        }
    }
}
