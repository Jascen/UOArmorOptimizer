using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
using Prism.Commands;
using System;
using System.Collections.Generic;

namespace ArmorOptimizer.ViewModels
{
    public class ConfigurationViewModel
    {
        private readonly DatabaseService _databaseService;

        public ConfigurationViewModel()
        {
            _databaseService = new DatabaseService();
            AddItemTypeCommand = new DelegateCommand(AddItemType, CanAddItemType);
            FindItemTypesCommand = new DelegateCommand(FindItemTypes, CanFindItemType);
            AddResourcesCommand = new DelegateCommand(AddResource, CanAddResource);
            FindResourcesCommand = new DelegateCommand(FindResources, CanFindResources);
        }

        public DelegateCommand AddItemTypeCommand { get; }
        public DelegateCommand AddResourcesCommand { get; }
        public DelegateCommand FindItemTypesCommand { get; }
        public DelegateCommand FindResourcesCommand { get; }

        public IEnumerable<ItemType> ItemTypes { get; protected set; }
        public ItemType ItemTypeToAdd { get; set; }
        public IEnumerable<Resource> Resources { get; protected set; }
        public Resource ResourceToAdd { get; set; }

        protected virtual void AddItemType()
        {
            if (!CanAddItemType()) throw new InvalidOperationException("Cannot bypass guard.");

            _databaseService.AddItemType(ItemTypeToAdd);
            ItemTypeToAdd = new ItemType();
        }

        protected virtual void AddResource()
        {
            if (!CanAddResource()) throw new InvalidOperationException("Cannot bypass guard.");

            _databaseService.AddResource(ResourceToAdd);
            ResourceToAdd = new Resource();
        }

        protected virtual bool CanAddItemType()
        {
            return true;
        }

        protected virtual bool CanAddResource()
        {
            return true;
        }

        protected virtual bool CanFindItemType()
        {
            return true;
        }

        protected virtual bool CanFindResources()
        {
            return true;
        }

        protected virtual void FindItemTypes()
        {
            if (!CanFindItemType()) throw new InvalidOperationException("Cannot bypass guard.");

            ItemTypes = _databaseService.FindAllItemTypes();
        }

        protected virtual void FindResources()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = _databaseService.FindAllResources();
        }
    }
}