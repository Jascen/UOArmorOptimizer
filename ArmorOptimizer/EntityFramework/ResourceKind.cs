using System;
using System.Collections.Generic;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ResourceKind
    {
        public ResourceKind()
        {
            ArmorType = new HashSet<ArmorType>();
            Resource = new HashSet<Resource>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public ICollection<ArmorType> ArmorType { get; set; }
        public ICollection<Resource> Resource { get; set; }
    }
}
