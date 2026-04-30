namespace OlmerovaIlkoGame
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ServerConfig s = new();
            Server server = new Server(s);

        }
    }
}