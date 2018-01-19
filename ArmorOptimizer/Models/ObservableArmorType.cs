using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ArmorOptimizer.Core.Enums;
using ArmorOptimizer.Properties;

namespace ArmorOptimizer.Models
{
    public class ObservableArmorType : INotifyPropertyChanged
    {
        private ObservableResistConfiguration _baseResist;
        private ResourceKind _baseResourceKind;
        private string _itemType;
        private string _name;
        private SlotTypes _slotType;

        public ObservableArmorType()
        {
            Item = new HashSet<Item>();
        }

        public ObservableResistConfiguration BaseResist
        {
            get { return _baseResist; }
            set
            {
                if (Equals(value, _baseResist)) return;
                _baseResist = value;
                OnPropertyChanged();
            }
        }

        public ResourceKind BaseResourceKind
        {
            get { return _baseResourceKind; }
            set
            {
                if (Equals(value, _baseResourceKind)) return;

                _baseResourceKind = value;
                OnPropertyChanged();
            }
        }

        public long Id { get; set; }

        public ICollection<Item> Item { get; set; }

        public string ItemType
        {
            get { return _itemType; }
            set
            {
                if (value == _itemType) return;

                _itemType = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;

                _name = value;
                OnPropertyChanged();
            }
        }

        public SlotTypes SlotType
        {
            get { return _slotType; }
            set
            {
                if (value == _slotType) return;

                _slotType = value;
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