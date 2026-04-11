using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlmerovaIlkoGame
{
    internal class Inventory
    {
        public List<Item> _items = new List<Item>();
        public int Capacity { get; set; } = 5;

        public IReadOnlyList<Item> Items => _items.AsReadOnly();
        public bool Add(Item item)
        {
            if (_items.Count >= Capacity) return false; // plný inventář
            _items.Add(item);
            return true;
        }
        public bool Remove(Item item)
        {
            return _items.Remove(item);
        }
        public Item? FindByTag(string tag)
        {
            return _items.FirstOrDefault(i => i.TagName == tag);
        }
        public bool HasItem(string tag)
        {
            return _items.Any(i => i.TagName == tag);
        }
    }


}
