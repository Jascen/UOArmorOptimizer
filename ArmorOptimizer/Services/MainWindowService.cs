using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.ViewModels;
using Prism.Commands;
using System;
using System.Collections.Generic;

namespace ArmorOptimizer.Services
{
    public class MainWindowService
    {
        protected readonly DatabaseService DatabaseService;

        protected readonly ImportingService ImportingService;
        protected readonly MainWindowViewModel Model;

        public MainWindowService(MainWindowViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            Model = model;
            DatabaseService = new DatabaseService();
            ImportingService = new ImportingService(DatabaseService);

            ImportCommand = new DelegateCommand(ImportItems, CanImportItems);
            FindResourcesCommand = new DelegateCommand(FindResources, CanFindResources);
            ApplyModifiersCommand = new DelegateCommand(ApplyModifiers, CanApplyModifiers);
        }

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
            return Model.SelectedSuit != null && Model.MaxResists != null;
        }

        #endregion Modifiers

        #region Resources

        public DelegateCommand FindResourcesCommand { get; }

        public IEnumerable<Resource> Resources { get; protected set; }

        protected virtual bool CanFindResources()
        {
            return true;
        }

        protected virtual void FindResources()
        {
            if (!CanFindResources()) throw new InvalidOperationException("Cannot bypass guard.");

            Resources = DatabaseService.FindAllResources();
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

        ///// <summary>
        ///// Open Details Window for the selected item
        ///// </summary>
        //public DelegateCommand ViewItemCommand { get; }

        ///// <summary>
        ///// Open Options Window
        ///// </summary>
        //public DelegateCommand ShowOptionsCommand { get; }

        //// Apply bonuses (VE, Wraith, MR/RA/Both)
        //public DelegateCommand ApplyBonusesCommand { get; }
    }
}