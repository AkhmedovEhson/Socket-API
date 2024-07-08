using Serilog;
using SocketServer.Server;
public class Program
{
    private static void LoggerConfigurations()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.Console()
                        .CreateLogger();
    }
    public static void Main(string[] args)
    {
        LoggerConfigurations();

        var _ = new Server(); // Server 
        _.StartListening();
    }
}