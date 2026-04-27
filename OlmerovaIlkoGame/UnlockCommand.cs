// UnlockCommand.cs
namespace OlmerovaIlkoGame
{
    internal class UnlockCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            // Odemykání je řešeno automaticky v MoveCommand
            await writer.WriteLineAsync("Místnosti se odemykají automaticky když máš správný předmět a jdeš do nich.");
        }
    }
}