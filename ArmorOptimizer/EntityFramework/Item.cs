using System;
using System.Collections.Generic;

namespace ArmorOptimizer.EntityFramework
{
    public partial class Item
    {
        public long Id { get; set; }
        public string UoId { get; set; }
        public long ArmorTypeId { get; set; }
        public long ResourceId { get; set; }
        public long NumberOfImbues { get; set; }
        public long LostResistancePoints { get; set; }
        public long PhysicalResist { get; set; }
        public long FireResist { get; set; }
        public long ColdResist { get; set; }
        public long PoisonResist { get; set; }
        public long EnergyResist { get; set; }

        public ArmorType ArmorType { get; set; }
        public Resource Resource { get; set; }
    }
}
