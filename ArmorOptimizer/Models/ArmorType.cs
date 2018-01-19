using System.ComponentModel.DataAnnotations.Schema;
using ArmorOptimizer.Core.Enums;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorType
    {
        [NotMapped]
        public SlotTypes SlotType
        {
            get => (SlotTypes)SlotId;
            set => SlotId = (long)value;
        }
    }
}