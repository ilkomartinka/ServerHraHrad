using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Room
    {
        public int RoomId { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        // Dictionary = klíč=směr, hodnota=id místnosti
        public Dictionary<string, int> AvailableRooms { get; set; } = new();
        public List<Item> ItemsInRoom { get; set; } = new();
        public List<Npc> NpcsInRoom { get; set; } = new();
        public bool IsLocked { get; set; } = false;
        public string? RequiredKeyTag { get; set; } // tag předmětu který otvírá tuto místnost


    }



}
