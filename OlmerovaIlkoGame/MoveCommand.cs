namespace OlmerovaIlkoGame
{
    internal class MoveCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            if (argument == "")
            {
                await writer.WriteLineAsync("Kam chceš jít? Použij: jdi <směr>");
                return;
            }

            Room currentRoom = player.CurrentRoom;

            // Existuje východ tímto směrem?
            if (!currentRoom.AvailableRooms.TryGetValue(argument, out int nextRoomId))
            {
                await writer.WriteLineAsync($"Na {argument} se nedá jít.");
                return;
            }

            Room nextRoom = world.AllRooms[nextRoomId];

            // Je místnost zamčená?
            if (nextRoom.IsLocked)
            {
                // Má hráč potřebný předmět?
                if (nextRoom.RequiredKeyTag == null || !player.Inventory.HasItem(nextRoom.RequiredKeyTag))
                {
                    // Speciální zpráva pro sklepení bez pochodně
                    if (nextRoom.RequiredKeyTag == "pochoden")
                        await writer.WriteLineAsync("Je tam tma jako v pytli. Bez světla tam nechoď!");
                    else
                        await writer.WriteLineAsync("Tato místnost je zamčená.");
                    return;
                }
            }

            // Přesuneme hráče
            player.CurrentRoom = nextRoom;
            player.CurrentRoomId = nextRoom.RoomId;

            // Zobrazíme novou místnost
            await GameLoop.ShowRoom(writer, player);
        }
    }
}