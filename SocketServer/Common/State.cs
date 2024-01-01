using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer.Common
{
    public struct State
    {
        public State() { }

        public byte[] buffer = new byte[1024];
        public StringBuilder sb = new StringBuilder();
        public Socket Listener { get; set; }

    }
}
