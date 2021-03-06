﻿using System;
using System.Collections.Generic;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ResistConfiguration
    {
        public ResistConfiguration()
        {
            ArmorType = new HashSet<ArmorType>();
            Resource = new HashSet<Resource>();
        }

        public long Id { get; set; }
        public long Physical { get; set; }
        public long Fire { get; set; }
        public long Cold { get; set; }
        public long Poison { get; set; }
        public long Energy { get; set; }

        public ICollection<ArmorType> ArmorType { get; set; }
        public ICollection<Resource> Resource { get; set; }
    }
}
