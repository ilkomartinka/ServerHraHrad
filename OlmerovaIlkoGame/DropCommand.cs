namespace OlmerovaIlkoGame
{
    internal class DropCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            if (argument == "")
            {
                await writer.WriteLineAsync("Co chceš položit? Použij: poloz <předmět>");
                return;
            }

            Item? item = player.Inventory.FindByTag(argument)
                ?? player.Inventory.Items.FirstOrDefault(i => i.Name.ToLower() == argument);

            if (item == null)
            {
                await writer.WriteLineAsync($"'{argument}' nemáš v inventáři.");
                return;
            }

            player.Inventory.Remove(item);
            player.CurrentRoom.ItemsInRoom.Add(item);
            await writer.WriteLineAsync($"Položil jsi: {item.Name}");
        }
    }
}