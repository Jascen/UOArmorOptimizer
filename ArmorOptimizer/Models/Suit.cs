using System.Collections.Generic;

namespace ArmorOptimizer.Models
{
    public class Suit
    {
        public int LostResistancePoints { get; set; }
        public int NumberOfImbues { get; set; }
        public List<ArmorThing> SuitPieces { get; set; }
        public Resistances TotalResistances { get; set; }
    }
}