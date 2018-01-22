using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Models
{
    public class ImbueableItem
    {
        public long LostResistancePoints { get; set; }
        public long NumberOfImbues { get; set; }
        public ResistConfiguration Resistances { get; set; }
        public string UoId { get; set; }
    }
}