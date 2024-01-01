// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {

        const string ip = "127.0.0.1";
        const int port = 8000;

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

        Socket client = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(endpoint);
        while (client.Connected)
        {
            Console.WriteLine("Connected");
            client.Send(Encoding.ASCII.GetBytes("Connected"));
            Thread.Sleep(1000);
        }
    }
}
