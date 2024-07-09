using Serilog;
using SocketClient.Security;
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
    /// <summary>
    /// `Server` - common component, with several APIs, especially for working with client's requests and communication towards server
    /// </summary>
    public partial class Server
    {
        // ManualResetEvent, if you do not know about manualResetEvent || multithreading, please read before looking to it....
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        // Hashing, common self-built component working especially with encrypting messages by ( AES256 ) enc-type....
        private Hashing hashing = new Hashing();
        public void StartListening()
        {
            // The ip address of server
            const string ip = "127.0.0.1";

            // The port of server
            const int port = 8000;

            // Endpoint config, server's IP address and PORT.
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

            // Simple abstraction, initialized the socket listener.
            Socket listener = new Socket(endpoint.AddressFamily,SocketType.Stream, ProtocolType.Tcp);

            // Status of current progress of server.
            bool running = true;

            // Note: So, logic of listening and handling next connections....
            try
            { 
                // Listener should know about the endpoint.
                listener.Bind(endpoint);

                // The limit how many connectors can listen.
                listener.Listen(100);


                Log.Information("Socket listening on 127.0.0.1:80 tcp/ip");

                // Note: Please read about `ManualResetEvent` and multithreading....
                // Todo: Use logs to capture each action in system....
                while (running)
                {
                    manualResetEvent.Reset();

                    Log.Information("Looking for a connection");
                    
                    // Starts accepting ANY connection !
                    listener.BeginAccept(Accept, listener);
                    
                    manualResetEvent.WaitOne();
               
                }
            }
            catch (Exception ex)
            {
                // Logging the reason of possible exception while accepting connection....
                Log.Error(ex.Message);
                throw;
            }
        }
       
    }
}
