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

        public async Task AddItemAsync(Item item)
        {
            using (var context = new ArmorOptimizerContext())
            {
                await context.Item.AddAsync(item);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddArmorTypeAsync(ArmorType armorType)
        {
            using (var context = new ArmorOptimizerContext())
            {
                await context.ArmorType.AddAsync(armorType);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddResourceAsync(Resource resource)
        {
            using (var context = new ArmorOptimizerContext())
            {
                await context.Resource.AddAsync(resource);
                await context.SaveChangesAsync();
            }
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