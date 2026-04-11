namespace OlmerovaIlkoGame
{
    internal abstract class Entity
    {
        protected string? TagName { get; set; }
        protected int Health { get; set; } = 100;
        protected Inventory Inventory { get; set; } = new Inventory();
        protected bool IsAlive => Health > 0;

    }
}
