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
    public partial class Server
    {
        private ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        public void StartListening()
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
                Console.WriteLine("Socket listening on 127.0.0.1:80 tcp/ip");
                while (running)
                {
                    manualResetEvent.Reset();

                    Console.WriteLine("Looking for a connection");
           
                    listener.BeginAccept(Accept, listener);
                    
                    manualResetEvent.WaitOne();
               
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
       
    }
}
