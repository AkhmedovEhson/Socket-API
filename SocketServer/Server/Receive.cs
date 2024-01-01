using SocketServer.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Server
{
    public partial class Server
    {
        public void Receive(IAsyncResult? result)
        {
            manualResetEvent.Set();
            Console.WriteLine("Receive");
            State data = (State)result.AsyncState;

            try
            {
                int read = data.Listener.EndReceive(result);

                if (read > 0)
                {
                    string message = Encoding.ASCII.GetString(data.buffer, 0, read);
                    Console.WriteLine($"Received message: '{message}'");

                }

                data.Listener.BeginReceive(data.buffer,
                       0,
                       1024,
                       0,
                       new AsyncCallback(Receive), data);

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message );
            }

        }
    }
}
