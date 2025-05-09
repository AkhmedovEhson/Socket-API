using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Utils
{
    public static class CustomLogger
    {
        public static ILogger Logger
        {
            get => _logger ?? throw new NullReferenceException("_logger is not registered !");
            set
            {
                _logger = value;
            }
        }

        private static ILogger? _logger;

        public static void Information(string method, string text)
        {
            Logger.Information($"[{method}]: {text}");
        }
        

        public static void Warning(string method, string text)
        {
            Logger.Warning($"[{method}]: {text}");
        }

        public static void Error(string method, string text)
        {
            Logger.Error($"[{method}]: {text}");
        }
    }
}
