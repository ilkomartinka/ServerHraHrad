namespace OlmerovaIlkoGame
{
    
        // writer = "trouba" pro posílání textu hráči (pro TCP server později)
        // player = kdo příkaz zadal
        // argument = co je za příkazem ("jdi SEVER" → argument = "sever")
        // world = přístup ke všem místnostem, NPC, předmětům
        internal interface ICommand
        {
            Task Execute(StreamWriter writer, Player player, string argument, World world);
        }
    
}