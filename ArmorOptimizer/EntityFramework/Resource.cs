using System;
using System.Collections.Generic;

namespace ArmorOptimizer.EntityFramework
{
    public partial class Resource
    {
        public Resource()
        {
            Item = new HashSet<Item>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long Color { get; set; }
        public long BonusResistId { get; set; }
        public long BaseResourceKindId { get; set; }

        public ResourceKind BaseResourceKind { get; set; }
        public ResistConfiguration BonusResist { get; set; }
        public ICollection<Item> Item { get; set; }
    }
}
