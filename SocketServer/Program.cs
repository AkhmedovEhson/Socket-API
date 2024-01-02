// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Logging;
using Serilog;
using SocketServer.Server;
using Serilog.Sinks.SystemConsole;
public class Program
{
    private static void LoggerConfigurations()
    {
        Log.Logger = new LoggerConfiguration().WriteTo
                .Console().CreateLogger();
    }
    public static void Main(string[] args)
    {
        LoggerConfigurations();

        var _ = new Server(); // Server 
        _.StartListening();
    }
}