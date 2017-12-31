using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArmorOptimizer.ViewModels
{
    public class ConfigurationViewModel
    {
        private readonly DatabaseService _databaseService;

        public ConfigurationViewModel()
        {
            _databaseService = new DatabaseService();
            AddArmorTypeCommand = new DelegateCommand(async () => await AddArmorTypeAsync(), CanAddArmorType);
            AddItemCommand = new DelegateCommand(async () => await AddItemAsync(), CanAddItem);
            FindItemTypesCommand = new DelegateCommand(async () => await FindArmorTypesAsync(), CanFindArmorTypes);
            AddResourcesCommand = new DelegateCommand(async () => await AddResourceAsync(), CanAddResource);
            FindResourcesCommand = new DelegateCommand(async () => await FindResourcesAsync(), CanFindResources);
        }

        public DelegateCommand AddArmorTypeCommand { get; }
        public DelegateCommand AddItemCommand { get; }
        public DelegateCommand AddResourcesCommand { get; }
        public IEnumerable<ArmorType> ArmorTypes { get; protected set; }
        public ArmorType ArmorTypeToAdd { get; set; }
        public DelegateCommand FindItemTypesCommand { get; }
        public DelegateCommand FindResourcesCommand { get; }
        public Item ItemToAdd { get; set; }
        public IEnumerable<Resource> Resources { get; protected set; }
        public Resource ResourceToAdd { get; set; }

        protected virtual async Task AddArmorTypeAsync()
        {
            if (!CanAddArmorType()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddArmorTypeAsync(ArmorTypeToAdd);
            ArmorTypeToAdd = new ArmorType();
        }

        protected virtual async Task AddItemAsync()
        {
            if (!CanAddItem()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddItemAsync(ItemToAdd);
            ItemToAdd = new Item();
        }

        protected virtual async Task AddResourceAsync()
        {
            if (!CanAddResource()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddResourceAsync(ResourceToAdd);
            ResourceToAdd = new Resource();
        }

        protected virtual bool CanAddArmorType()
        {
            return true;
        }

        protected virtual bool CanAddItem()
        {
            return true;
        }

        protected virtual bool CanAddResource()
        {
            return true;
        }

        protected virtual bool CanFindArmorTypes()
        {
            return true;
        }

        protected virtual bool CanFindResources()
        {
            return true;
        }

        protected virtual async Task FindArmorTypesAsync()
        {
            if (!CanFindArmorTypes()) throw new InvalidOperationException("Cannot bypass guard.");

            ArmorTypes = await _databaseService.FindAllArmorTypesAsync();
        }

        protected virtual async Task FindResourcesAsync()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = await _databaseService.FindAllResourcesAsync();
        }
    }
}