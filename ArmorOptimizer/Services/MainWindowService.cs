using ArmorOptimizer.Models;
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
            var buffedResistances = new Resistances
            {
                PhysicalResist = baseResistances.PhysicalResist,
                FireResist = baseResistances.FireResist,
                ColdResist = baseResistances.ColdResist,
                PoisonResist = baseResistances.PoisonResist,
                EnergyResist = baseResistances.EnergyResist,
            };

            if (Model.IsVampiricForm)
            {
                buffedResistances.FireResist -= 25;
            }
            else if (Model.IsWraithForm)
            {
                buffedResistances.PhysicalResist += 15;
                buffedResistances.FireResist -= 5;
                buffedResistances.EnergyResist -= 5;
            }

            if (Model.HasCorpseSkin)
            {
                buffedResistances.PhysicalResist += 10;
                buffedResistances.FireResist -= 15;
                buffedResistances.ColdResist += 10;
                buffedResistances.PoisonResist -= 15;
            }

            if (Model.HasMagicReflection)
            {
                buffedResistances.PhysicalResist -= 20;
                buffedResistances.FireResist += 10;
                buffedResistances.ColdResist += 10;
                buffedResistances.PoisonResist += 10;
                buffedResistances.EnergyResist += 10;
            }

            if (Model.HasProtection)
            {
                buffedResistances.PhysicalResist -= 15;
            }

            if (Model.HasReactiveArmor)
            {
                buffedResistances.PhysicalResist += 20;
                buffedResistances.FireResist -= 5;
                buffedResistances.ColdResist -= 5;
                buffedResistances.PoisonResist -= 5;
                buffedResistances.EnergyResist -= 5;
            }

            var maxPhysical = Model.HasMagicReflection ? Model.MaxResists.PhysicalResist - 5 : Model.MaxResists.PhysicalResist;
            buffedResistances.PhysicalResist = buffedResistances.PhysicalResist < 1 ? 0 : Math.Min(buffedResistances.PhysicalResist, maxPhysical);
            buffedResistances.FireResist = buffedResistances.FireResist < 1 ? 0 : Math.Min(buffedResistances.FireResist, Model.MaxResists.FireResist);
            buffedResistances.ColdResist = buffedResistances.ColdResist < 1 ? 0 : Math.Min(buffedResistances.ColdResist, Model.MaxResists.ColdResist);
            buffedResistances.PoisonResist = buffedResistances.PoisonResist < 1 ? 0 : Math.Min(buffedResistances.PoisonResist, Model.MaxResists.PoisonResist);
            buffedResistances.EnergyResist = buffedResistances.EnergyResist < 1 ? 0 : Math.Min(buffedResistances.EnergyResist, Model.MaxResists.EnergyResist);
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