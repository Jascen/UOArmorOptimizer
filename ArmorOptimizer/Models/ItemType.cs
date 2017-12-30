namespace ArmorOptimizer.Models
{
    public class ItemType
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public long SlotId { get; set; }
        public ResourceKind BaseResourceKind { get; set; }
        public Resistances BaseResistances { get; set; }
    }
}