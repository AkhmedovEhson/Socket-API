using Serilog;
using SocketServer.Server;
using Serilog.Sinks.SystemConsole;
using Security.Utils;
public class Program
{
    private static void LoggerConfigurations()
    {
        CustomLogger.Logger = new CLogger(
            new LoggerConfiguration().WriteTo
                .Console().CreateLogger());
 
    }
    public static async Task Main(string[] args)
    {
        LoggerConfigurations();

        var _ = new Server(); // Server 
        await _.StartListening();
    }
}