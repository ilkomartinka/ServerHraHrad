namespace OlmerovaIlkoGame
{
    internal class GameLoop
    {
        private readonly World _world;

        // Slovník příkazů — klíč = co hráč napíše, hodnota = objekt příkazu
        private readonly Dictionary<string, ICommand> _commands;

        public GameLoop(World world)
        {
            _world = world;

            // Registrujeme všechny příkazy
            _commands = new Dictionary<string, ICommand>
            {
                { "jdi",      new MoveCommand() },
                { "vezmi",    new TakeCommand() },
                { "poloz",    new DropCommand() },
                { "mluv",     new TalkToCommand() },
                { "pouzij",   new UseCommand() },
                { "inventar", new InventoryCommand() },
                { "pomoc",    new HelpCommand() },
            };
        }

        public async Task Run(StreamWriter writer, Player player)
        {
            // Zobrazíme místnost při startu
            await ShowRoom(writer, player);

            // Hlavní herní smyčka
            while (!player.HasWon)
            {
                await writer.WriteAsync("\n> ");

                // Čteme příkaz — pro teď z Console, později z TCP
                string? input = Console.ReadLine();
                if (input == null) break;

                input = input.Trim().ToLower();
                if (input.Length == 0) continue;

                // Rozložíme vstup na příkaz a argument
                // "jdi sever"   → command="jdi",    argument="sever"
                // "vezmi mec"   → command="vezmi",  argument="mec"  
                // "inventar"    → command="inventar", argument=""
                string[] parts = input.Split(' ', 2);
                string command = parts[0];
                string argument = parts.Length > 1 ? parts[1] : "";

                if (_commands.TryGetValue(command, out ICommand? cmd))
                {
                    await cmd.Execute(writer, player, argument, _world);

                    // Po každé akci zkontrolujeme výhru
                    if (player.HasWon)
                    {
                        await writer.WriteLineAsync("\n╔══════════════════════════════╗");
                        await writer.WriteLineAsync("║     GRATULUJEME! VYHRÁLI!    ║");
                        await writer.WriteLineAsync("╚══════════════════════════════╝");
                        await writer.WriteLineAsync("Přinesl jsi korunu králi a zachránil království!");
                        break;
                    }
                }
                else
                {
                    await writer.WriteLineAsync($"Neznámý příkaz '{command}'. Napiš 'pomoc' pro seznam příkazů.");
                }
            }
        }
     
            // Zobrazí popis aktuální místnosti
            public static async Task ShowRoom(StreamWriter writer, Player player)
            {
                Room room = player.CurrentRoom;
                await writer.WriteLineAsync($"\n=== {room.Name.ToUpper()} ===");
                await writer.WriteLineAsync(room.Description);

                await writer.WriteLineAsync("\nVýchody: " + string.Join(", ", room.AvailableRooms.Keys));

                if (room.ItemsInRoom.Count > 0)
                {
                    string items = string.Join(", ", room.ItemsInRoom.Select(i => i.Name));
                    await writer.WriteLineAsync("Předměty na zemi: " + items);
                }

                if (room.NpcsInRoom.Count > 0)
                {
                    string npcs = string.Join(", ", room.NpcsInRoom.Select(n => n.Name));
                    await writer.WriteLineAsync("Osoby v místnosti: " + npcs);
                }

                // ← ДОДАНО: nápověda příkazů pod každou místností
                await writer.WriteLineAsync("\n--- příkazy ---");
                await writer.WriteLineAsync("jdi <směr> | vezmi <předmět> | poloz <předmět>");
                await writer.WriteLineAsync("mluv <jméno> | pouzij <předmět> | inventar | pomoc");
            }
        }
    }
