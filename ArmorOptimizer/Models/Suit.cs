using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;

namespace ArmorOptimizer.Models
{
    public class Suit
    {
        public Item Arms { get; set; }
        public Item Chest { get; set; }
        public Item Gloves { get; set; }
        public Item Helm { get; set; }
        public List<Item> Items { get; set; }
        public Item Legs { get; set; }
        public int LostResistancePoints { get; set; }
        public Item Misc { get; set; }
        public int NumberOfImbues { get; set; }
        public ResistConfiguration TotalResistances { get; set; }
    }
}