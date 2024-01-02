// See https://aka.ms/new-console-template for more information

using SocketClient.Security;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        var hashing = new Hashing();

        const string ip = "127.0.0.1";
        const int port = 8000;

        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

        Socket client = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        client.Connect(endpoint);
        string? input = string.Empty;

        
        while (client.Connected)
        {
            check:
                Console.Write("~root: ");
                input = Console.ReadLine();

            if (input == string.Empty)
            {
                goto check;
            }

            client.Send(Convert.FromBase64String(hashing.Encryption(input)));
        }
    }
}
