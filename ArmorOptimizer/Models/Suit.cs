using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;

namespace ArmorOptimizer.Models
{
    public class Suit
    {
        public int LostResistancePoints { get; set; }
        public int NumberOfImbues { get; set; }
        public List<Item> SuitPieces { get; set; }
        public ResistConfiguration TotalResistances { get; set; }
    }
}