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
        public Item? Weapon { get; set; }
        // Dialogy: "klic" -> "text"
        // "default" -> "Nemám co říct."
        // "pozdrav" -> "Dobrý den!"
        public Dictionary<string, string> Dialogs { get; set; } = new();
        public string GetDialog(string key = "default")
        {
            if (Dialogs.TryGetValue(key, out string? text))
                return text;
            return "...";   // NPC mlčí
        }
    }
}
