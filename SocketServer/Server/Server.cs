using Security.Utils;
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
    using static CustomLogger;
    public partial class Server
    {
        private Semaphore _semaphore = new(1,1);
        private Hashing hashing = new();

        public async Task StartListening()
        {
            const string ip = "127.0.0.1";
            const int port = 8000;

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);
            Socket listener = new Socket(endpoint.AddressFamily,SocketType.Stream, ProtocolType.Tcp);
            bool running = true;
            try
            { 
                listener.Bind(endpoint);
                listener.Listen(100);
                Logger.Information(nameof(StartListening), "Socket listening on 127.0.0.1:80 tcp/ip");
                
                while (running)
                {
                    _semaphore.WaitOne();
                    Logger.Information(nameof(StartListening), "Looking for a connection");
               
                    listener.BeginAccept(Accept, listener);                    
                }
            }
            catch (Exception ex)
            {
                Logger.Error(nameof(StartListening),ex.Message);
                throw;
            }
        }
       
    }
}
