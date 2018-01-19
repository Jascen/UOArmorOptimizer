using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.ViewModels;
using Prism.Commands;
using System;
using System.Threading.Tasks;
using ArmorOptimizer.Properties;

namespace ArmorOptimizer.Services
{
    public class ConfigurationService
    {
        protected readonly ConfigurationViewModel Model;
        private readonly DatabaseService _databaseService;

        public ConfigurationService([NotNull] ConfigurationViewModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));
            _databaseService = new DatabaseService();

            AddArmorTypeCommand = new DelegateCommand(async () => await AddArmorTypeAsync(), CanAddArmorType);
            AddItemCommand = new DelegateCommand(async () => await AddItemAsync(), CanAddItem);
            AddResourcesCommand = new DelegateCommand(async () => await AddResourceAsync(), CanAddResource);
        }

        protected virtual async Task AddArmorTypeAsync()
        {
            if (!CanAddArmorType()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddArmorTypeAsync(Model.ArmorTypeToAdd);
            Model.ArmorTypeToAdd = new ArmorType();
        }

        protected virtual async Task AddItemAsync()
        {
            if (!CanAddItem()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddItemAsync(Model.ItemToAdd);
            Model.ItemToAdd = new Item();
        }

        protected virtual async Task AddResourceAsync()
        {
            if (!CanAddResource()) throw new InvalidOperationException("Cannot bypass guard.");

            await _databaseService.AddResourceAsync(Model.ResourceToAdd);
            Model.ResourceToAdd = new Resource();
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

        #region Commands

        public DelegateCommand AddArmorTypeCommand { get; }
        public DelegateCommand AddItemCommand { get; }
        public DelegateCommand AddResourcesCommand { get; }

        #endregion Commands
    }
}