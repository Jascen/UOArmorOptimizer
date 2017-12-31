using ArmorOptimizer.Enums;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorType
    {
        public SlotTypes SlotType
        {
            get => (SlotTypes)SlotId;
            set => SlotId = (long)value;
        }
    }
}