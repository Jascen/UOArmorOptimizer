using ArmorOptimizer.Enums;

namespace ArmorOptimizer.EntityFramework
{
    public partial class ArmorType
    {
        public SlotTypes SlotType
        {
            get => (SlotTypes)Slot;
            set => Slot = (long)value;
        }
    }
}