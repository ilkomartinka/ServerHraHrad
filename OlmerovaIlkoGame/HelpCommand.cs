namespace OlmerovaIlkoGame
{
    internal class HelpCommand : ICommand
    {
        public async Task Execute(StreamWriter writer, Player player, string argument, World world)
        {
            await writer.WriteLineAsync("\n--- NÁPOVĚDA ---");
            await writer.WriteLineAsync("  jdi <směr>       — pohyb (sever, jih, vychod, zapad)");
            await writer.WriteLineAsync("  vezmi <předmět>  — vezmi předmět ze země");
            await writer.WriteLineAsync("  poloz <předmět>  — polož předmět na zem");
            await writer.WriteLineAsync("  mluv <jméno>     — promluv s postavou");
            await writer.WriteLineAsync("  pouzij <předmět> — použij předmět");
            await writer.WriteLineAsync("  inventar         — zobraz inventář");
            await writer.WriteLineAsync("  pomoc            — zobraz tuto nápovědu");
        }
    }
}