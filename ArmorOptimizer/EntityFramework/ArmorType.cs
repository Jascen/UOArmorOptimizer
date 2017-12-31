using System;
using System.Collections.Generic;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorType
    {
        public ArmorType()
        {
            Item = new HashSet<Item>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public long SlotId { get; set; }
        public long BaseResistId { get; set; }
        public long BaseResourceKindId { get; set; }

        public ResistConfiguration BaseResist { get; set; }
        public ResourceKind BaseResourceKind { get; set; }
        public ICollection<Item> Item { get; set; }
    }
}
