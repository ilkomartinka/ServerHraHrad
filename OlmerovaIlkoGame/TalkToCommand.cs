namespace OlmerovaIlkoGame
{
    internal class TalkToCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            if (argument == "")
            {
                await writer.WriteLineAsync("S kým chceš mluvit? Použij: mluv <jméno>");
                return;
            }

            // Hledáme NPC v aktuální místnosti podle tagu nebo jména
            Npc? npc = player.CurrentRoom.NpcsInRoom.FirstOrDefault(n =>
                n.TagName == argument || n.Name.ToLower() == argument);

            if (npc == null)
            {
                await writer.WriteLineAsync($"Žádný '{argument}' tady není.");
                return;
            }

            // Případ 1: NPC už dal předmět — říká "already_given" dialog
            if (npc.AlreadyGaveItem)
            {
                await writer.WriteLineAsync(npc.GetDialog("already_given"));
                return;
            }

            // Případ 2: NPC vyžaduje předmět od hráče
            if (npc.RequiredItemTag != null)
            {
                // Má hráč potřebný předmět?
                Item? requiredItem = player.Inventory.FindByTag(npc.RequiredItemTag);

                if (requiredItem == null)
                {
                    // Hráč nemá předmět → NPC řekne default dialog
                    await writer.WriteLineAsync(npc.GetDialog("default"));
                    return;
                }

                // Hráč MÁ předmět → NPC reaguje na "has_item"
                await writer.WriteLineAsync(npc.GetDialog("has_item"));

                // Odebereme předmět z inventáře hráče (hráč ho "dal" NPC)
                player.Inventory.Remove(requiredItem);
                await writer.WriteLineAsync($"[Předal jsi: {requiredItem.Name}]");

                // NPC dá hráči předmět jako odměnu
                if (npc.GivesItemTag != null)
                {
                    Item? reward = world.AllItems[npc.GivesItemTag];
                    if (player.Inventory.Add(reward))
                        await writer.WriteLineAsync($"[Dostal jsi: {reward.Name}]");
                    else
                        await writer.WriteLineAsync("Inventář je plný! Předmět leží na zemi.");
                    player.CurrentRoom.ItemsInRoom.Add(reward);
                }

                npc.AlreadyGaveItem = true;
                return;
            }

            // Případ 3: NPC dává předmět bez podmínky (např. Sluha)
            if (npc.GivesItemTag != null && !npc.AlreadyGaveItem)
            {
                await writer.WriteLineAsync(npc.GetDialog("default"));

                Item? gift = world.AllItems[npc.GivesItemTag];
                if (player.Inventory.Add(gift))
                    await writer.WriteLineAsync($"[Dostal jsi: {gift.Name}]");
                else
                {
                    await writer.WriteLineAsync("Inventář je plný! Předmět leží na zemi.");
                    player.CurrentRoom.ItemsInRoom.Add(gift);
                }

                npc.AlreadyGaveItem = true;
                return;
            }

            // Případ 4: NPC jen mluví (žádné předměty)
            await writer.WriteLineAsync(npc.GetDialog("default"));
        }
    }
}