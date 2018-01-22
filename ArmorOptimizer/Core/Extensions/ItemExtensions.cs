using ArmorOptimizer.EntityFramework;

namespace ArmorOptimizer.Core.Extensions
{
    public static class ItemExtensions
    {
        //TODO: Is this even necessary?  Can we use a lighter weight object instead?
        public static Item Clone(this Item item)
        {
            return new Item
            {
                Id = item.Id,
                ArmorTypeId = item.ArmorTypeId,
                ArmorType = item.ArmorType,
                UoId = item.UoId,
                ResourceId = item.ResourceId,
                Resource = item.Resource,
                LostResistancePoints = item.LostResistancePoints,
                NumberOfImbues = item.NumberOfImbues,
                PhysicalResist = item.PhysicalResist,
                FireResist = item.FireResist,
                ColdResist = item.ColdResist,
                PoisonResist = item.PoisonResist,
                EnergyResist = item.EnergyResist,
            };
        }
    }
}