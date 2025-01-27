using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Security.Configurations
{
    public class Configuration
    {
        private string _file = string.Empty;

        public Configuration(string path,string? filename)
        {
            _file = File.ReadAllText(Path.Combine(path, filename ?? "appsettings.json"));
        }

        public Configuration(string path):this(path,null)
        {}

        public T GetObject<T>()
        {
            return JsonSerializer.Deserialize<T>(_file) ?? throw new ArgumentNullException("Object can not be found in config. file");
        }
    }
}
