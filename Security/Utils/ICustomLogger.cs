using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Utils
{
    public interface ICustomLogger : ILogger
    {
        public void Information(string method, string text);


        public void Warning(string method, string text);

        public void Error(string method, string text);
    }
}

