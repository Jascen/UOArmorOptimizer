using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Models;
using ArmorOptimizer.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArmorOptimizer.Services
{
    public class MainWindowService
    {
        protected readonly DatabaseService DatabaseService;
        protected readonly ImportingService ImportingService;
        protected readonly MainWindowViewModel Model;
        protected readonly OptimizingService OptimizingService;

        public MainWindowService(MainWindowViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Model = model;
            DatabaseService = new DatabaseService();
            ImportingService = new ImportingService(DatabaseService);
            OptimizingService = new OptimizingService(1000);

            ImportCommand = new DelegateCommand(ImportItems, CanImportItems);
            FindResourcesCommand = new DelegateCommand(async () => await FindResourcesAsync(), CanFindResources);
            OptimizeSuitCommand = new DelegateCommand(async () => await OptimizeSuitAsync(), CanOptimizeSuit);
            ApplyModifiersCommand = new DelegateCommand(ApplyModifiers, CanApplyModifiers);
        }

        #region Optimizing

        public DelegateCommand OptimizeSuitCommand { get; }

        public bool CanOptimizeSuit()
        {
            return true;
        }

        public async Task OptimizeSuitAsync()
        {
            if (Model.AllItems == null)
            {
                Model.AllItems = await DatabaseService.FindAllItemsAsync();
                if (Model.AllItems == null || !Model.AllItems.Any())
                {
                    var items = new List<Item>();
                    var sampleResists = new ResistConfiguration
                    {
                        Physical = 11,
                        Fire = 12,
                        Cold = 13,
                        Poison = 14,
                        Energy = 15,
                    };
                    items.Add(CheaterMethod(SlotTypes.Helm, sampleResists, "Verite"));
                    items.Add(CheaterMethod(SlotTypes.Chest, sampleResists, "Verite"));
                    items.Add(CheaterMethod(SlotTypes.Arms, sampleResists, "Verite"));
                    items.Add(CheaterMethod(SlotTypes.Gloves, sampleResists, "Barbed"));
                    items.Add(CheaterMethod(SlotTypes.Legs, sampleResists, "Verite"));
                    Model.AllItems = items;
                }
            }

            var categorizedItems = new CategorizedItems();
            foreach (var item in Model.AllItems)
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

        protected Item CheaterMethod(SlotTypes slot, ResistConfiguration resists, string resourceName)
        {
            return new Item
            {
                UoId = "XXXXXX",
                ArmorType = new ArmorType
                {
                    SlotId = (long)slot
                },
                PhysicalResist = resists.Physical,
                FireResist = resists.Fire,
                ColdResist = resists.Cold,
                PoisonResist = resists.Poison,
                EnergyResist = resists.Energy,
                Resource = new Resource { Name = resourceName }
            };
        }

        #endregion Optimizing

        #region Modifiers

        public DelegateCommand ApplyModifiersCommand { get; }

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

        #endregion Modifiers

        #region Resources

        public DelegateCommand FindResourcesCommand { get; }

        public IEnumerable<Resource> Resources { get; protected set; }

        protected virtual bool CanFindResources()
        {
            return true;
        }

        protected virtual async Task FindResourcesAsync()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = await DatabaseService.FindAllResourcesAsync();
        }

        #endregion Resources

        #region Import Items

        public string FileToImport { get; set; }
        public DelegateCommand ImportCommand { get; }

        protected virtual bool CanImportItems()
        {
            return true;
        }

        protected virtual void ImportItems()
        {
            if (!CanImportItems()) throw new InvalidOperationException("Cannot bypass guard.");

            ImportingService.Import(FileToImport);
        }

        #endregion Import Items
    }
}