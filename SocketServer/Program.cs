// See https://aka.ms/new-console-template for more information

using SocketServer.Server;

public class Program
{
    public static void Main(string[] args)
    {
        var _ = new Server();
        _.StartListening();
    }
}