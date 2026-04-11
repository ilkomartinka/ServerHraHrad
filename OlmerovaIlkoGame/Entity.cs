namespace OlmerovaIlkoGame
{
    internal abstract class Entity
    {
        public string? TagName { get; set; }
        public int Health { get; set; } = 100;
        public Inventory Inventory { get; set; } = new Inventory();
        public bool IsAlive => Health > 0;

    }
}
