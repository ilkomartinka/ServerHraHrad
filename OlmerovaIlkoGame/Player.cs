using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Player : Entity
    {
        public Room CurrentRoom { get; set; }
        public Item? CurrentItem { get; set ; }

        public string Name { get; set; } = "";
        public string PasswordHash { get; set; } = "";
        public int CurrentRoomId { get; set; } = 1;
        public bool HasWon { get; set; } = false;

        public Player(Room startRoom)
        {
            CurrentRoom = startRoom;
            CurrentRoomId = startRoom.RoomId;
        }
        public bool HasItem(string tag) => Inventory.HasItem(tag);
    }
}
