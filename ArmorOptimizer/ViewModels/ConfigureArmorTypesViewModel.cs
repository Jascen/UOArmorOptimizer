using ArmorOptimizer.Annotations;
using ArmorOptimizer.Core.Enums;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmorOptimizer.ViewModels
{
    public class ConfigureArmorTypesViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ArmorType> _armorTypes;
        private ObservableArmorType _editableArmorType;
        private IEnumerable<ResourceKind> _resourceKinds;
        private ArmorType _selectedArmorType;

        public ConfigureArmorTypesViewModel()
        {
            Service = new ConfigureArmorTypesService(this);
        }

        public IEnumerable<ArmorType> ArmorTypes
        {
            get => _armorTypes;
            set
            {
                if (Equals(value, _armorTypes)) return;

                _armorTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableArmorType EditableArmorType
        {
            get => _editableArmorType;
            set
            {
                if (Equals(value, _editableArmorType)) return;

                _editableArmorType = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<ResourceKind> ResourceKinds
        {
            get => _resourceKinds;
            set
            {
                if (Equals(value, _resourceKinds)) return;

                _resourceKinds = value;
                OnPropertyChanged();
            }
        }

        public ArmorType SelectedArmorType
        {
            get => _selectedArmorType;
            set
            {
                if (Equals(value, _selectedArmorType)) return;

                _selectedArmorType = value;
                OnPropertyChanged();
            }
        }

        public ConfigureArmorTypesService Service { get; }
        public IEnumerable<SlotTypes> SlotTypes { get; set; }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion INotifyPropertyChanged
    }
}