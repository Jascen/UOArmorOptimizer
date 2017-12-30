using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Models
{
    public class Resource
    {
        public ResistConfiguration BonusResistances { get; set; }
        public string Name { get; set; }
        public ResourceKind ResourceKind { get; set; }
    }
}