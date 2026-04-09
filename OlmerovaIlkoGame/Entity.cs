namespace OlmerovaIlkoGame
{
    internal abstract class Entity
    {
        protected string? TagName { get; set; }
        protected int Health { get; set; }
        protected Inventory Inventory { get; set; }
        protected bool IsAlive { get; set; }
    }
}
