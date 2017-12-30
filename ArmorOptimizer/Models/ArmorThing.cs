using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Enums;

namespace ArmorOptimizer.Models
{
    public class ArmorThing
    {
        public string Id { get; set; } = "XXXXXX";
        public ResistConfiguration Resistances { get; set; }
        public Resource Resource { get; set; }
        public SlotTypes SlotType { get; set; }
    }
}