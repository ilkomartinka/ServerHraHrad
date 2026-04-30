using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Server
    {
        private TcpListener myServer;
        private bool isRunning;
        private int port;

        public Server(ServerConfig serverConfig)
        {
            ServerConfig serverCon = serverConfig.LoadPort("data/settings.json");
            port = serverCon.Port;
            myServer = new TcpListener(System.Net.IPAddress.Any, port);
            myServer.Start();
            isRunning = true;
            ServerLoop();
        }

        private void ServerLoop()
        {
            Console.WriteLine("Server byl spusten");
            while (isRunning)
            {
                TcpClient client = myServer.AcceptTcpClient();
                Thread thread = new Thread(new ParameterizedThreadStart(ClientLoop));
                thread.Start(client);
            }

        }

        private void ClientLoop(object obj)
        {
            TcpClient client = (TcpClient)obj;
            StreamReader reader = new StreamReader(client.GetStream(), Encoding.UTF8);
            StreamWriter writer = new StreamWriter(client.GetStream(), Encoding.UTF8);
            writer.AutoFlush = true;

            try
            {
                World world = World.Load("data/");
                Room startRoom = world.AllRooms[1];

                Player player = new Player(startRoom) { Name = "Hráč" };

                GameLoop gameLoop = new GameLoop(world);
                gameLoop.Run(reader, writer, player).Wait();

                Console.WriteLine("\nKonec hry.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba: " + ex.Message);
            }
            finally
            {
                client.Close();
                Console.WriteLine("Klient odpojen");
            }
        }
    }
}
