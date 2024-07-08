using SocketClient.Security;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        // Hashing, common self-built component working especially with encrypting messages by(AES256) enc - type ....
        var hashing = new Hashing();

        // Ip address of the server connecting to.
        const string ip = "127.0.0.1";

        // Port 
        const int port = 8000;

        // Endpoint of server. 
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(ip), port);

        // Client's connection
        Socket client = new Socket(endpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        // Start connecting :)
        client.Connect(endpoint);

        // Current client's message.
        string? input = string.Empty;
        
        // While client is connected to server, he can send message.
        while (client.Connected)
        {
            check:
                Console.Write("~root: ");
                input = Console.ReadLine();

            // Forces to type new message if user's input is empty !
            if (string.IsNullOrEmpty(input))
            {
                goto check;
            }

            // Finally sends message to server ....
            client.Send(Convert.FromBase64String(hashing.Encryption(input)));
        }
    }
}
