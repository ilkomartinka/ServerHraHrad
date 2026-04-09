using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Player : Entity
    {
        public required Room CurrentRoom { get; set; }
        public Item? CurrentItem { get; set ; }

        public Player(Room currentRoom, Item? currentItem)
        {
            this.CurrentRoom = currentRoom;
            this.CurrentItem = currentItem;
        }
    }
}
