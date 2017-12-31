using ArmorOptimizer.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public Task<List<ArmorType>> FindAllArmorTypesAsync()
        {
            using (var context = new ArmorOptimizerContext())
            {
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                return context.ArmorType
                    .Include(r => r.BaseResist)
                    .Include(r => r.BaseResourceKind)
                    .Select(recs => recs).ToListAsync();
            }
        }

        public Task<List<Item>> FindAllItemsAsync()
        {
            using (var context = new ArmorOptimizerContext())
            {
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                return context.Item
                    .Include(r => r.ArmorType)
                    .Include(r => r.Resource).ThenInclude(r => r.BaseResourceKind)
                    .Include(r => r.Resource).ThenInclude(r => r.BonusResist)
                    .Select(recs => recs).ToListAsync();
            }
        }

        public Task<List<Resource>> FindAllResourcesAsync()
        {
            using (var context = new ArmorOptimizerContext())
            {
                context.ChangeTracker.AutoDetectChangesEnabled = false;
                return context.Resource
                    .Include(r => r.BaseResourceKind)
                    .Include(r => r.BonusResist)
                    .Select(recs => recs).ToListAsync();
            }
        }
    }
}