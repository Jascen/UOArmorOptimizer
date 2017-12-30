using ArmorOptimizer.Models;
using System.Collections.Generic;

namespace ArmorOptimizer.Services
{
    public class DatabaseService
    {
        public const string Database = @"ArmorOptimizer.db";

        public void AddItemType(ItemType itemType)
        {
        }

        public void AddResource(Resource resource)
        {
        }

        public IEnumerable<ItemType> FindAllItemTypes()
        {
            return new List<ItemType>();
        }

        public IEnumerable<Resource> FindAllResources()
        {
            return new List<Resource>();
        }
    }
}