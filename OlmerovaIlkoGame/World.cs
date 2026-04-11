using System.Text.Json;

namespace OlmerovaIlkoGame
{
    internal class World
    {
        
        public Dictionary<int, Room> AllRooms { get; private set; } = new();

        
        public Dictionary<string, Item> AllItems { get; private set; } = new();

     
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

     
        public static World Load(string dataPath)
        {
            var world = new World();
            world.LoadItems(Path.Combine(dataPath, "items.json"));
            world.LoadRooms(Path.Combine(dataPath, "rooms.json"));
            return world;
        }

        private void LoadItems(string path)
        {
            string json = File.ReadAllText(path);

            List<Item> items = JsonSerializer.Deserialize<List<Item>>(json, _jsonOptions)!;

            foreach (Item item in items)
            {
               
                AllItems[item.TagName] = item;
            }

            Console.WriteLine($"Načteno {AllItems.Count} předmětů.");
        }

        private void LoadRooms(string path)
        {
            string json = File.ReadAllText(path);

            List<RoomData> roomDataList = JsonSerializer.Deserialize<List<RoomData>>(json, _jsonOptions)!;

            foreach (RoomData data in roomDataList)
            {
                Room room = new Room
                {
                    RoomId = data.RoomId,
                    Name = data.Name,
                    Description = data.Description,
                    AvailableRooms = data.AvailableRooms,
                    IsLocked = data.IsLocked,
                    RequiredKeyTag = data.RequiredKeyTag
                };

                foreach (string tag in data.ItemsInRoom)
                {
                    if (AllItems.TryGetValue(tag, out Item? item))
                        room.ItemsInRoom.Add(item);
                    else
                        Console.WriteLine($"VAROVÁNÍ: předmět '{tag}' nenalezen!");
                }

                AllRooms[room.RoomId] = room;
            }

            Console.WriteLine($"Načteno {AllRooms.Count} místností.");
        }

        // Pomocná třída — pouze pro čtení JSON
       
        private class RoomData
        {
            public int RoomId { get; set; }
            public string Name { get; set; } = "";
            public string Description { get; set; } = "";
            public Dictionary<string, int> AvailableRooms { get; set; } = new();
            public List<string> ItemsInRoom { get; set; } = new();   
            public List<string> NpcsInRoom { get; set; } = new();   
            public bool IsLocked { get; set; } = false;
            public string? RequiredKeyTag { get; set; }
        }
    }
}