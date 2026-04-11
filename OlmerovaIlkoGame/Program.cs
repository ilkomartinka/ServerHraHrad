namespace OlmerovaIlkoGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            World world = World.Load("data/");

            // --- PŘEDMĚTY ---
            Console.WriteLine("--- PŘEDMĚTY ---");
            foreach (var item in world.AllItems.Values)
            {
                Console.WriteLine($"  [{item.TagName}] {item.Name}");
                Console.WriteLine($"      Zbraň: {item.IsWeapon}, Klíč: {item.IsKey}, Bonus: {item.AttackBonus}");
            }

            // --- NPC ---
            Console.WriteLine("\n--- NPC ---");
            foreach (var npc in world.AllNpcs.Values)
            {
                Console.WriteLine($"  [{npc.TagName}] {npc.Name}");
                Console.WriteLine($"      Přátelský: {npc.IsFriendly}, HP: {npc.Health}");
                Console.WriteLine($"      Potřebuje: {npc.RequiredItemTag ?? "nic"}");
                Console.WriteLine($"      Dává: {npc.GivesItemTag ?? "nic"}");
                Console.WriteLine($"      Dialogy: {string.Join(", ", npc.Dialogs.Keys)}");
                Console.WriteLine($"      Říká: {npc.GetDialog()}");
            }

            // --- MÍSTNOSTI ---
            Console.WriteLine("\n--- MÍSTNOSTI ---");
            foreach (var room in world.AllRooms.Values)
            {
                Console.WriteLine($"  [{room.RoomId}] {room.Name}");
                Console.WriteLine($"      Zamčená: {room.IsLocked}, Klíč: {room.RequiredKeyTag ?? "žádný"}");

                // Předměty v místnosti
                string items = room.ItemsInRoom.Count > 0
                    ? string.Join(", ", room.ItemsInRoom.Select(i => i.Name))
                    : "žádné";
                Console.WriteLine($"      Předměty: {items}");

                // NPC v místnosti
                string npcs = room.NpcsInRoom.Count > 0
                    ? string.Join(", ", room.NpcsInRoom.Select(n => n.Name))
                    : "žádné";
                Console.WriteLine($"      NPC: {npcs}");

                // Východy
                string exits = string.Join(", ", room.AvailableRooms.Select(e => $"{e.Key}→{e.Value}"));
                Console.WriteLine($"      Východy: {exits}");
            }

            // --- KONTROLA LOGIKY ---
            Console.WriteLine("\n--- KONTROLA LOGIKY ---");
            bool ok = true;

            foreach (var npc in world.AllNpcs.Values)
            {
                // Kontrola: existuje předmět který NPC dává?
                if (npc.GivesItemTag != null && !world.AllItems.ContainsKey(npc.GivesItemTag))
                {
                    Console.WriteLine($"  ❌ {npc.Name}: givesItemTag '{npc.GivesItemTag}' neexistuje v items.json!");
                    ok = false;
                }

                // Kontrola: existuje předmět který NPC vyžaduje?
                if (npc.RequiredItemTag != null && !world.AllItems.ContainsKey(npc.RequiredItemTag))
                {
                    Console.WriteLine($"  ❌ {npc.Name}: requiredItemTag '{npc.RequiredItemTag}' neexistuje v items.json!");
                    ok = false;
                }

                // Kontrola: má NPC dialog "has_item" pokud vyžaduje předmět?
                if (npc.RequiredItemTag != null && !npc.Dialogs.ContainsKey("has_item"))
                {
                    Console.WriteLine($"  ⚠️  {npc.Name}: má requiredItemTag ale chybí dialog 'has_item'!");
                    ok = false;
                }
            }

            foreach (var room in world.AllRooms.Values)
            {
                // Kontrola: existuje klíč který otvírá místnost?
                if (room.RequiredKeyTag != null && !world.AllItems.ContainsKey(room.RequiredKeyTag))
                {
                    Console.WriteLine($"  ❌ Místnost '{room.Name}': requiredKeyTag '{room.RequiredKeyTag}' neexistuje v items.json!");
                    ok = false;
                }

                // Kontrola: vedou východy na existující místnosti?
                foreach (var exit in room.AvailableRooms)
                {
                    if (!world.AllRooms.ContainsKey(exit.Value))
                    {
                        Console.WriteLine($"  ❌ Místnost '{room.Name}': východ '{exit.Key}' vede na neexistující místnost {exit.Value}!");
                        ok = false;
                    }
                }
            }

            if (ok)
                Console.WriteLine("  ✅ Vše v pořádku!");

            Console.WriteLine("\nStiskni Enter...");
            Console.ReadLine();
        }
    }
}