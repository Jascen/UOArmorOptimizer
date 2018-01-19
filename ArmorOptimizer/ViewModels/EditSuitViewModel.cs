using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using ArmorOptimizer.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmorOptimizer.ViewModels
{
    public class EditSuitViewModel : INotifyPropertyChanged
    {
        private IEnumerable<ArmorType> _armorTypes;
        private bool _isEditMode;
        private IEnumerable<Resource> _resources;
        private ArmorType _selectedArmorType;
        private Item _selectedItem;
        private Suit _selectedSuit;

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

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (value == _isEditMode) return;

                _isEditMode = value;
                OnPropertyChanged();
            }
        }

        public IEnumerable<Resource> Resources
        {
            get => _resources;
            set
            {
                if (Equals(value, _resources)) return;

                _resources = value;
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

        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (Equals(value, _selectedItem)) return;

                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public Suit SelectedSuit
        {
            get => _selectedSuit;
            set
            {
                if (Equals(value, _selectedSuit)) return;

                _selectedSuit = value;
                OnPropertyChanged();
            }
        }

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