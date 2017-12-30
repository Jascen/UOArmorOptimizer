using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class ArmorThing
    {
        public string Id { get; set; } = "XXXXXX";
        public Resistances Resistances { get; set; }
        public Resource Resource { get; set; }
        public SlotType SlotType { get; set; }
    }
}