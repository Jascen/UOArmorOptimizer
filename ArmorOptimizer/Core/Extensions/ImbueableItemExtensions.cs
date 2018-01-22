using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;

namespace ArmorOptimizer.Core.Extensions
{
    public static class ImbueableItemExtensions
    {
        public static ImbueableItem Clone(this ImbueableItem item)
        {
            return new ImbueableItem
            {
                UoId = item.UoId,
                LostResistancePoints = item.LostResistancePoints,
                NumberOfImbues = item.NumberOfImbues,
                Resistances = new ResistConfiguration
                {
                    Physical = item.Resistances.Physical,
                    Fire = item.Resistances.Fire,
                    Cold = item.Resistances.Cold,
                    Poison = item.Resistances.Poison,
                    Energy = item.Resistances.Energy,
                },
            };
        }
    }
}