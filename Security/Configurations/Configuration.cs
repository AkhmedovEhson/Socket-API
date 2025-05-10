using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Security.Configurations
{
    public class Configuration(string path,string? filename)
    {
        private readonly string _file = File.ReadAllText(Path.Combine(path, filename ?? "appsettings.json"));

        public Configuration(string path) : this(path, null)
        { }

        public T GetObject<T>()
        {
            return JsonSerializer.Deserialize<T>(_file) ?? throw new ArgumentNullException("Object can not be found in config. file");
        }
    }
}