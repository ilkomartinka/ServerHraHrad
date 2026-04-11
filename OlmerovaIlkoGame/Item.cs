using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Item
    {
        public string TagName { get; set; } = "";      // "pochoden"
        public string Name { get; set; } = "";          // "Pochodeň"
        public string Description { get; set; } = "";   // "Stará dřevěná pochodeň"
        public bool IsKey { get; set; } = false;        // otvírá zamčenou místnost?
        public bool IsWeapon { get; set; } = false;     // lze použít v boji?
        public int AttackBonus { get; set; } = 0;
    }
}
