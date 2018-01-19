using ArmorOptimizer.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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