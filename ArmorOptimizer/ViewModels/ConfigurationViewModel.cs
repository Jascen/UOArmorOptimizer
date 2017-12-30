﻿using ArmorOptimizer.EntityFramework;
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
            AddItemTypeCommand = new DelegateCommand(AddArmorType, CanAddArmorType);
            FindItemTypesCommand = new DelegateCommand(FindArmorTypes, CanFindArmorTypes);
            AddResourcesCommand = new DelegateCommand(AddResource, CanAddResource);
            FindResourcesCommand = new DelegateCommand(FindResources, CanFindResources);
        }

        public DelegateCommand AddItemTypeCommand { get; }
        public DelegateCommand AddResourcesCommand { get; }
        public IEnumerable<ArmorType> ArmorTypes { get; protected set; }
        public ArmorType ArmorTypeToAdd { get; set; }
        public DelegateCommand FindItemTypesCommand { get; }
        public DelegateCommand FindResourcesCommand { get; }
        public IEnumerable<Resource> Resources { get; protected set; }
        public Resource ResourceToAdd { get; set; }

        protected virtual void AddArmorType()
        {
            if (!CanAddArmorType()) throw new InvalidOperationException("Cannot bypass guard.");

            _databaseService.AddItemType(ArmorTypeToAdd);
            ArmorTypeToAdd = new ArmorType();
        }

        protected virtual void AddResource()
        {
            if (!CanAddResource()) throw new InvalidOperationException("Cannot bypass guard.");

            _databaseService.AddResource(ResourceToAdd);
            ResourceToAdd = new Resource();
        }

        protected virtual bool CanAddArmorType()
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

        protected virtual void FindArmorTypes()
        {
            if (!CanFindArmorTypes()) throw new InvalidOperationException("Cannot bypass guard.");

            ArmorTypes = _databaseService.FindAllArmorTypes();
        }

        protected virtual void FindResources()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = _databaseService.FindAllResources();
        }
    }
}