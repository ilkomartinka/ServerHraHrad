using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Npc : Entity
    {
        public bool IsFriendly { get; set; }
        public Item Weapon { get; set; }
    }
}
