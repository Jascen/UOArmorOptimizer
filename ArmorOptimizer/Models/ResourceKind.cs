using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Models
{
    public class ResourceKind
    {
        public ResistConfiguration BaseResistances { get; set; }
        public string Name { get; set; }
    }
}