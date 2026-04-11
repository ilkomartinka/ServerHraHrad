using System.Text.Json;
using YamlDotNet.Core;

namespace OlmerovaIlkoGame
{
    internal class World
    {
        public Dictionary<int, Room> AllRooms { get; private set; } = new();
        public Dictionary<string, Item> AllItems { get; private set; } = new();

      
        public Dictionary<string, Npc> AllNpcs { get; private set; } = new();

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public static World Load(string dataPath)
        {
            var world = new World();
            world.LoadItems(Path.Combine(dataPath, "items.json"));
            world.LoadNpcs(Path.Combine(dataPath, "npc.json"));   
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

       
        private void LoadNpcs(string path)
        {
            string json = File.ReadAllText(path);
            List<NpcData> npcDataList = JsonSerializer.Deserialize<List<NpcData>>(json, _jsonOptions)!;

            foreach (NpcData data in npcDataList)
            {
                Npc npc = new Npc
                {
                    TagName = data.TagName,
                    Name = data.Name,
                    Health = data.Health,
                    IsFriendly = data.IsFriendly,
                    GivesItemTag = data.GivesItemTag,
                    RequiredItemTag = data.RequiredItemTag,
                    Dialogs = data.Dialogs
                };

                
                if (data.WeaponTag != null && AllItems.TryGetValue(data.WeaponTag, out Item? weapon))
                    npc.Weapon = weapon;

                AllNpcs[data.TagName] = npc;
            }

            Console.WriteLine($"Načteno {AllNpcs.Count} NPC.");
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

               
                foreach (string tag in data.NpcsInRoom)
                {
                    if (AllNpcs.TryGetValue(tag, out Npc? npc))
                        room.NpcsInRoom.Add(npc);
                    else
                        Console.WriteLine($"VAROVÁNÍ: NPC '{tag}' nenalezeno!");
                }

                AllRooms[room.RoomId] = room;
            }

            Console.WriteLine($"Načteno {AllRooms.Count} místností.");
        }

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

        private class NpcData
        {
            public string TagName { get; set; } = "";
            public string Name { get; set; } = "";
            public int Health { get; set; } = 100;
            public bool IsFriendly { get; set; } = true;
            public string? WeaponTag { get; set; }
            public string? GivesItemTag { get; set; }
            public string? RequiredItemTag { get; set; }
            public Dictionary<string, string> Dialogs { get; set; } = new();
        }
    }
}