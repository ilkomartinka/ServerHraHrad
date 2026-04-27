namespace OlmerovaIlkoGame
{
    internal class InventoryCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            await writer.WriteLineAsync($"\n--- INVENTÁŘ ({player.Inventory.Items.Count}/{player.Inventory.Capacity}) ---");

            if (player.Inventory.Items.Count == 0)
            {
                await writer.WriteLineAsync("Inventář je prázdný.");
                return;
            }

            foreach (Item item in player.Inventory.Items)
            {
                string info = "";
                if (item.IsWeapon) info += " [zbraň]";
                if (item.IsKey) info += " [klíč]";
                await writer.WriteLineAsync($"  • {item.Name}{info}");
                await writer.WriteLineAsync($"    {item.Description}");
            }
        }
    }
}