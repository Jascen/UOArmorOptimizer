using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;

namespace ArmorOptimizer.Models
{
    public class CategorizedItems
    {
        public List<Item> Arms { get; set; } = new List<Item>();
        public List<Item> Chests { get; set; } = new List<Item>();
        public List<Item> Gloves { get; set; } = new List<Item>();
        public List<Item> Helms { get; set; } = new List<Item>();
        public List<Item> Legs { get; set; } = new List<Item>();
        public List<Item> Misc { get; set; } = new List<Item>();
    }
}