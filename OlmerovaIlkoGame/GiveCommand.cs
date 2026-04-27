// GiveCommand.cs
namespace OlmerovaIlkoGame
{
    internal class GiveCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            // Dávání předmětů NPC je řešeno v TalkToCommand
            await writer.WriteLineAsync("Pro předání předmětu NPC použij: mluv <jméno>");
        }
    }
}