using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class ServerConfig
    {
        public int Port { set; get; }

        public ServerConfig LoadPort(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<ServerConfig>(json);
        }
    }
}
