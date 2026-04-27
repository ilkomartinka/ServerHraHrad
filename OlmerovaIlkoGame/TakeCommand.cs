namespace OlmerovaIlkoGame
{
    internal class TakeCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            if (argument == "")
            {
                await writer.WriteLineAsync("Co chceš vzít? Použij: vezmi <předmět>");
                return;
            }

            Room room = player.CurrentRoom;

            // Hledáme předmět v místnosti podle tagu nebo jména
            Item? item = room.ItemsInRoom.FirstOrDefault(i =>
                i.TagName == argument || i.Name.ToLower() == argument);

            if (item == null)
            {
                await writer.WriteLineAsync($"Žádný '{argument}' tady není.");
                return;
            }

            // Zkusíme přidat do inventáře
            if (!player.Inventory.Add(item))
            {
                await writer.WriteLineAsync("Inventář je plný!");
                return;
            }

            // Odebereme z místnosti
            room.ItemsInRoom.Remove(item);
            await writer.WriteLineAsync($"Vzal jsi: {item.Name}");
        }
    }
}