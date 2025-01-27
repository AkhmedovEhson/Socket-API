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
        /// <summary>
        /// `Receive` - receives message from connection ( tcp/ip )
        /// </summary>
        /// <param name="result"></param>
        public void Receive(IAsyncResult? result)
        {
            State data = (State)result!.AsyncState!;

            // Adjusting the received message </>
            try
            {
                int read = data.Listener.EndReceive(result);
 
                if (read > 0)
                {
                    string message = Encoding.UTF8.GetString(data.buffer, 0, read);
                    Log.Information($"Received from client: {message}");
                }

                data.Listener.BeginReceive(data.buffer,
                       offset:0,
                       size:1024,
                       socketFlags:0,
                       callback:new AsyncCallback(Receive), data);

            }
            catch(Exception e)
            {
                Log.Error("Ooops, got exception in receiving message time ....");
                throw;
            }

        }
    }
}
