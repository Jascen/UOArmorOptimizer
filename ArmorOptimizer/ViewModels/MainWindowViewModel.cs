using ArmorOptimizer.Annotations;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmorOptimizer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ResistConfiguration _buffedResistances = new ResistConfiguration();
        private Suit _selectedSuit;
        private IEnumerable<Suit> _suitPermutations;

        public MainWindowViewModel()
        {
            Service = new MainWindowService(this);

            TargetResists = new ResistConfiguration
            {
                Physical = 85,
                Fire = 90,
                Cold = 60,
                Poison = 60,
                Energy = 65,
            };
            MaxResists = new ResistConfiguration
            {
                Physical = 70,
                Fire = 70,
                Cold = 70,
                Poison = 70,
                Energy = 75,
            };
        }

        public IEnumerable<Item> AllItems { get; set; }

        public ResistConfiguration BuffedResistances
        {
            get => _buffedResistances;
            set
            {
                if (Equals(value, _buffedResistances)) return;

                _buffedResistances = value;
                OnPropertyChanged();
            }
        }

        public bool HasCorpseSkin { get; set; }
        public bool HasMagicReflection { get; set; } = true;
        public bool HasProtection { get; set; }
        public bool HasReactiveArmor { get; set; } = true;
        public bool IsBaseForm { get; set; } = true;
        public bool IsVampiricForm { get; set; }
        public bool IsWraithForm { get; set; }
        public ResistConfiguration MaxResists { get; set; }

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

        public MainWindowService Service { get; }

        public IEnumerable<Suit> SuitPermutations
        {
            get => _suitPermutations;
            set
            {
                if (Equals(value, _suitPermutations)) return;

                _suitPermutations = value;
                OnPropertyChanged();
            }
        }

        public ResistConfiguration TargetResists { get; set; }

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