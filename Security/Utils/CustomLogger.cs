using Security.Utils;
using Serilog;
using Serilog.Core;
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
        public static ICustomLogger Logger
        {
            get
            {
                return _logger ?? throw new NullReferenceException("_logger is not registered !");
            }
            set
            {
               
                _logger ??= value;
            }
        }

        private static ICustomLogger? _logger;

    }

    public class CLogger : ICustomLogger
    {

        private readonly ILogger _logger;


        public CLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void Information(string method, string text)
        {
            _logger.Information($"[{method}]: {text}");
        }


        public void Warning(string method, string text)
        {
            _logger.Warning($"[{method}]: {text}");
        }

        public void Error(string method, string text)
        {
            _logger.Error($"[{method}]: {text}");
        }

        public void Write(LogEvent logEvent)
        {
            _logger.Write(logEvent);
        }
    }

}


