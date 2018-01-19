using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using ArmorOptimizer.ViewModels;
using ArmorOptimizer.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ArmorOptimizer.Core.Enums;

namespace ArmorOptimizer.Services
{
    public class MainWindowService
    {
        protected readonly DatabaseService DatabaseService;
        protected readonly MainWindowViewModel Model;
        protected readonly OptimizingService OptimizingService;

        public MainWindowService(MainWindowViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Model = model;
            DatabaseService = new DatabaseService();
            OptimizingService = new OptimizingService(1000);

            WindowLoadedCommand = new DelegateCommand(WindowLoaded);
            ConfigureSettingsCommand = new DelegateCommand(ConfigureSettings);
            ApplyModifiersCommand = new DelegateCommand(ApplyModifiers, CanApplyModifiers);
            FindArmorTypesCommand = new DelegateCommand(async () => await FindArmorTypesAsync(), CanFindArmorTypes);
            FindResourceKindsCommand = new DelegateCommand(async () => await FindResourceKindsAsync(), CanFindResourceKinds);
            FindResourcesCommand = new DelegateCommand(async () => await FindResourcesAsync(), CanFindResources);
            InspectSuitCommand = new DelegateCommand(InspectSuit, CanInspectSuit);
            OptimizeSuitCommand = new DelegateCommand(async () => await OptimizeSuitAsync(), CanOptimizeSuit);
        }

        public IEnumerable<Item> AllItems { get; set; }
        public IEnumerable<ArmorType> ArmorTypes { get; protected set; }
        public string FileToImport { get; set; }
        public IEnumerable<ResourceKind> ResourceKinds { get; protected set; }
        public IEnumerable<Resource> Resources { get; protected set; }

        #region Commands

        public DelegateCommand ApplyModifiersCommand { get; }
        public DelegateCommand ConfigureSettingsCommand { get; }
        public DelegateCommand FindArmorTypesCommand { get; }
        public DelegateCommand FindResourceKindsCommand { get; }
        public DelegateCommand FindResourcesCommand { get; }
        public DelegateCommand InspectSuitCommand { get; }
        public DelegateCommand OptimizeSuitCommand { get; }
        public DelegateCommand WindowLoadedCommand { get; }

        #endregion Commands

        public bool CanInspectSuit()
        {
            return true;
            // TODO: Figure out why this isn't properly firing
            //return Model.SelectedItem != null;
        }

        public bool CanOptimizeSuit()
        {
            return true;
        }

        public void ConfigureSettings()
        {
            //TODO: This is totally the wrong way to do this.
            var configurationView = new ConfigurationView
            {
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
            };
            var configureArmorTypesViewModel = ((ConfigurationViewModel)configurationView.DataContext).ConfigureArmorTypesViewModel;
            configureArmorTypesViewModel.ArmorTypes = ArmorTypes;
            configureArmorTypesViewModel.ResourceKinds = ResourceKinds;
            configureArmorTypesViewModel.SlotTypes = new List<SlotTypes> { SlotTypes.Helm, SlotTypes.Chest, SlotTypes.Arms, SlotTypes.Gloves, SlotTypes.Legs, SlotTypes.Misc };
            configurationView.ShowDialog();
        }

        public void InspectSuit()
        {
            //TODO: This is totally the wrong way to do this.
            var editSuitViewModel = new EditSuitViewModel
            {
                SelectedSuit = Model.SelectedSuit,
                Resources = Resources,
                SelectedArmorType = Model.SelectedItem.ArmorType,
                ArmorTypes = ArmorTypes,
            };
            var editSuitView = new EditSuitView()
            {
                DataContext = editSuitViewModel,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Owner = Application.Current.MainWindow,
            };
            editSuitView.ShowDialog();
        }

        public async Task OptimizeSuitAsync()
        {
            // Fetch all itemd from DB if not yet retrieved.
            if (AllItems == null)
            {
                AllItems = await DatabaseService.FindAllItemsAsync();
            }

            var categorizedItems = new CategorizedItems();
            foreach (var item in AllItems)
            {
                switch (item.ArmorType.SlotType)
                {
                    case SlotTypes.Helm:
                        categorizedItems.Helms.Add(item);
                        break;

                    case SlotTypes.Chest:
                        categorizedItems.Chests.Add(item);
                        break;

                    case SlotTypes.Arms:
                        categorizedItems.Arms.Add(item);
                        break;

                    case SlotTypes.Gloves:
                        categorizedItems.Gloves.Add(item);
                        break;

                    case SlotTypes.Legs:
                        categorizedItems.Legs.Add(item);
                        break;

                    case SlotTypes.Misc:
                        categorizedItems.Misc.Add(item);
                        break;

                    case SlotTypes.Unknown:
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            var betterSuit = OptimizingService.OptimizeSuit(Model.TargetResists, categorizedItems, Model.SelectedSuit, out var suitPermutations);
            if (betterSuit == null) return;

            Model.SelectedSuit = null;
            Model.SuitPermutations = suitPermutations;

            if (ApplyModifiersCommand.CanExecute())
            {
                ApplyModifiersCommand.Execute();
            }
        }

        public void WindowLoaded()
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                while (!FindResourcesCommand.CanExecute())
                {
                }

                FindResourcesCommand.Execute();

                while (!FindArmorTypesCommand.CanExecute())
                {
                }

                FindArmorTypesCommand.Execute();

                while (!FindResourceKindsCommand.CanExecute())
                {
                }

                FindResourceKindsCommand.Execute();
            }
            finally
            {
                Mouse.OverrideCursor = null;
            }
        }

        protected virtual void ApplyModifiers()
        {
            if (!CanApplyModifiers()) throw new InvalidOperationException("Cannot bypass guard.");

            var baseResistances = Model.SelectedSuit.TotalResistances;
            var buffedResistances = new ResistConfiguration
            {
                Physical = baseResistances.Physical,
                Fire = baseResistances.Fire,
                Cold = baseResistances.Cold,
                Poison = baseResistances.Poison,
                Energy = baseResistances.Energy,
            };

            if (Model.IsVampiricForm)
            {
                buffedResistances.Fire -= 25;
            }
            else if (Model.IsWraithForm)
            {
                buffedResistances.Physical += 15;
                buffedResistances.Fire -= 5;
                buffedResistances.Energy -= 5;
            }

            if (Model.HasCorpseSkin)
            {
                buffedResistances.Physical += 10;
                buffedResistances.Fire -= 15;
                buffedResistances.Cold += 10;
                buffedResistances.Poison -= 15;
            }

            if (Model.HasMagicReflection)
            {
                buffedResistances.Physical -= 20;
                buffedResistances.Fire += 10;
                buffedResistances.Cold += 10;
                buffedResistances.Poison += 10;
                buffedResistances.Energy += 10;
            }

            if (Model.HasProtection)
            {
                buffedResistances.Physical -= 15;
            }

            if (Model.HasReactiveArmor)
            {
                buffedResistances.Physical += 20;
                buffedResistances.Fire -= 5;
                buffedResistances.Cold -= 5;
                buffedResistances.Poison -= 5;
                buffedResistances.Energy -= 5;
            }

            var maxPhysical = Model.HasMagicReflection ? Model.MaxResists.Physical - 5 : Model.MaxResists.Physical;
            buffedResistances.Physical = buffedResistances.Physical < 1 ? 0 : Math.Min(buffedResistances.Physical, maxPhysical);
            buffedResistances.Fire = buffedResistances.Fire < 1 ? 0 : Math.Min(buffedResistances.Fire, Model.MaxResists.Fire);
            buffedResistances.Cold = buffedResistances.Cold < 1 ? 0 : Math.Min(buffedResistances.Cold, Model.MaxResists.Cold);
            buffedResistances.Poison = buffedResistances.Poison < 1 ? 0 : Math.Min(buffedResistances.Poison, Model.MaxResists.Poison);
            buffedResistances.Energy = buffedResistances.Energy < 1 ? 0 : Math.Min(buffedResistances.Energy, Model.MaxResists.Energy);
            Model.BuffedResistances = buffedResistances;
        }

        protected virtual bool CanApplyModifiers()
        {
            return Model.MaxResists != null;
        }

        protected virtual bool CanFindArmorTypes()
        {
            return true;
        }

        protected virtual bool CanFindResourceKinds()
        {
            return true;
        }

        protected virtual bool CanFindResources()
        {
            return true;
        }

        protected virtual bool CanImportItems()
        {
            return true;
        }

        protected virtual async Task FindArmorTypesAsync()
        {
            if (!CanFindArmorTypes()) throw new InvalidOperationException("Cannot bypass guard.");

            ArmorTypes = await DatabaseService.FindAllArmorTypesAsync();
        }

        protected virtual async Task FindResourceKindsAsync()
        {
            if (!CanFindResourceKinds()) throw new InvalidOperationException("Cannot bypass guard.");

            ResourceKinds = await DatabaseService.FindAllResourceKindsAsync();
        }

        protected virtual async Task FindResourcesAsync()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = await DatabaseService.FindAllResourcesAsync();
        }
    }
}