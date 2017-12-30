namespace ArmorOptimizer.Models
{
    public class ItemType
    {
        public Resistances BaseResistances { get; set; }
        public ResourceKind BaseResourceKind { get; set; }
        public long Id { get; set; }
        public long SlotId { get; set; }
        public string Type { get; set; }
    }
}