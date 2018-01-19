using ArmorOptimizer.Core.Enums;
using ArmorOptimizer.Models;
using ArmorOptimizer.Properties;
using ArmorOptimizer.ViewModels;
using Prism.Commands;
using System;
using System.ComponentModel;
using System.Linq;

namespace ArmorOptimizer.Services
{
    public class ConfigureArmorTypesService
    {
        protected readonly ConfigureArmorTypesViewModel Model;

        public ConfigureArmorTypesService([NotNull] ConfigureArmorTypesViewModel model)
        {
            Model = model ?? throw new ArgumentNullException(nameof(model));

            //TODO: Feels hacky
            Model.PropertyChanged += ModelOnPropertyChanged;

            UpdateSelectedArmorTypeCommand = new DelegateCommand(UpdateSelectedArmorType, CanUpdateSelectedArmorType);
        }

        public DelegateCommand UpdateSelectedArmorTypeCommand { get; }

        protected virtual bool CanUpdateSelectedArmorType()
        {
            return Model.SelectedArmorType != null;
        }

        protected virtual void UpdateSelectedArmorType()
        {
            if (!CanUpdateSelectedArmorType()) throw new InvalidOperationException("Cannot bypass guard.");

            var selectedModel = Model.SelectedArmorType;
            var selectedBaseResource = Model.ResourceKinds.FirstOrDefault(r => r.Id == selectedModel.BaseResourceKindId);
            if (selectedBaseResource == null) throw new ArgumentException("Failed to locate Base Resource Kind.");

            var selectedSlotType = Model.SlotTypes.FirstOrDefault(s => (int)s == selectedModel.SlotId);
            if (selectedSlotType is default(SlotTypes)) throw new ArgumentException("Failed to locate Slot Type.");

            var newResist = new ObservableResistConfiguration
            {
                Physical = selectedModel.BaseResist.Physical,
                Fire = selectedModel.BaseResist.Fire,
                Cold = selectedModel.BaseResist.Cold,
                Poison = selectedModel.BaseResist.Poison,
                Energy = selectedModel.BaseResist.Energy,
            };
            var editableModel = new ObservableArmorType
            {
                Id = selectedModel.Id,
                BaseResist = newResist,
                Name = selectedModel.Name,
                SlotType = selectedSlotType,
                BaseResourceKind = selectedBaseResource,
                ItemType = selectedModel.ItemType,
            };

            Model.EditableArmorType = editableModel;
        }

        private void ModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName != nameof(Model.SelectedArmorType)) return;

            if (UpdateSelectedArmorTypeCommand.CanExecute())
            {
                UpdateSelectedArmorTypeCommand.Execute();
            }
        }
    }
}