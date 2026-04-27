namespace OlmerovaIlkoGame
{
    internal class UseCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            if (argument == "")
            {
                await writer.WriteLineAsync("Co chceš použít? Použij: pouzij <předmět>");
                return;
            }

            // Hledáme předmět v inventáři
            Item? item = player.Inventory.FindByTag(argument)
                ?? player.Inventory.Items.FirstOrDefault(i => i.Name.ToLower() == argument);

            if (item == null)
            {
                await writer.WriteLineAsync($"'{argument}' nemáš v inventáři.");
                return;
            }

            // === LOGIKA POUŽITÍ ===

            // Použití zbraně — hledáme nepřátelské NPC v místnosti
            if (item.IsWeapon)
            {
                Npc? enemy = player.CurrentRoom.NpcsInRoom.FirstOrDefault(n => !n.IsFriendly && n.IsAlive);

                if (enemy == null)
                {
                    await writer.WriteLineAsync($"Mácháš {item.Name} do vzduchu. Není tu nikdo k boji.");
                    return;
                }

                // === BÓJ ===
                await writer.WriteLineAsync($"\n⚔️  Útočíš na {enemy.Name} s {item.Name}!");
                await writer.WriteLineAsync(enemy.GetDialog("has_item"));

                // Jednoduchý boj — hráč vždy vyhraje pokud má zbraň
                int damage = 50 + item.AttackBonus;
                enemy.Health -= damage;

                await writer.WriteLineAsync($"Způsobil jsi {damage} poškození!");

                if (!enemy.IsAlive)
                {
                    // Hráč vyhrál boj
                    await writer.WriteLineAsync($"\n{enemy.GetDialog("defeat")}");

                    // NPC dá předmět po porážce
                    if (enemy.GivesItemTag != null)
                    {
                        Item? loot = world.AllItems[enemy.GivesItemTag];
                        player.Inventory.Add(loot);
                        await writer.WriteLineAsync($"[Dostal jsi: {loot.Name}]");

                        // Koruna = výhra!
                        if (loot.TagName == "koruna")
                            player.HasWon = true;
                    }

                    // Odstraníme mrtvé NPC z místnosti
                    player.CurrentRoom.NpcsInRoom.Remove(enemy);
                }
                else
                {
                    await writer.WriteLineAsync($"{enemy.Name} má stále {enemy.Health} HP.");
                }

                return;
            }

            // Použití klíče — otevře zamčenou místnost
            if (item.IsKey)
            {
                // Hledáme zamčenou místnost v dostupných východech
                bool unlocked = false;
                foreach (var exit in player.CurrentRoom.AvailableRooms)
                {
                    Room neighbor = world.AllRooms[exit.Value];
                    if (neighbor.IsLocked && neighbor.RequiredKeyTag == item.TagName)
                    {
                        neighbor.IsLocked = false;
                        await writer.WriteLineAsync($"Odemkl jsi: {neighbor.Name}!");
                        unlocked = true;
                        break;
                    }
                }

                if (!unlocked)
                    await writer.WriteLineAsync($"{item.Name} tu nic neodemyká.");

                return;
            }

            // Ostatní předměty
            await writer.WriteLineAsync($"Prohlédneš si {item.Name}. {item.Description}");
        }
    }
}