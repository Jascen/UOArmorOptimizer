using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;

namespace ArmorOptimizer.Services
{
    public class DatabaseService
    {
        public const string Database = @"ArmorOptimizer.db";

        public void AddItemType(ArmorType armorType)
        {
        }

        public void AddResource(Resource resource)
        {
        }

        public IEnumerable<ArmorType> FindAllArmorTypes()
        {
            return new List<ArmorType>();
        }

        public IEnumerable<Resource> FindAllResources()
        {
            return new List<Resource>();
        }
    }
}