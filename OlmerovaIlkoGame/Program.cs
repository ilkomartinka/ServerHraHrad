namespace OlmerovaIlkoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
            World world = World.Load("data/");
            //test
            Console.WriteLine("\n--- MÍSTNOSTI ---");
            foreach (var room in world.AllRooms.Values)
            {
                Console.WriteLine($"[{room.RoomId}] {room.Name}");
                Console.WriteLine($"Popis: {room.Description}");
                Console.Write("Předměty: ");
                Console.WriteLine(room.ItemsInRoom.Count > 0
                    ? string.Join(", ", room.ItemsInRoom.Select(i => i.Name))
                    : "žádné");
                Console.Write("Východy: ");
                Console.WriteLine(string.Join(", ", room.AvailableRooms.Keys));
                Console.WriteLine();
            }

            Console.WriteLine("Stiskni Enter pro ukončení...");
            Console.ReadLine();
        }
    }
}