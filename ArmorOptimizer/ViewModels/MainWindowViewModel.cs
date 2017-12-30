using ArmorOptimizer.Annotations;
using ArmorOptimizer.Builders;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Models;
using ArmorOptimizer.Services;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ArmorOptimizer.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private Resistances _buffedResistances;
        private Suit _selectedSuit;
        private IEnumerable<Suit> _suitPermutations;

        public MainWindowViewModel()
        {
            Service = new MainWindowService(this);

            var suitBuilder = new SuitBuilder();
            var sampleResists = new Resistances
            {
                PhysicalResist = 11,
                FireResist = 12,
                ColdResist = 13,
                PoisonResist = 14,
                EnergyResist = 15,
            };
            suitBuilder.AddHelm(new ArmorThing { SlotType = SlotType.Helm, Resistances = sampleResists, Resource = new Resource { Name = "Verite" } });
            suitBuilder.AddChest(new ArmorThing { SlotType = SlotType.Chest, Resistances = sampleResists, Resource = new Resource { Name = "Verite" } });
            suitBuilder.AddArms(new ArmorThing { SlotType = SlotType.Arms, Resistances = sampleResists, Resource = new Resource { Name = "Verite" } });
            suitBuilder.AddGloves(new ArmorThing { SlotType = SlotType.Gloves, Resistances = sampleResists, Resource = new Resource { Name = "Barbed" } });
            suitBuilder.AddLegs(new ArmorThing { SlotType = SlotType.Legs, Resistances = sampleResists, Resource = new Resource { Name = "Verite" } });
            var suitPermutations = new List<Suit>();
            for (var i = 0; i < 100; i++)
            {
                suitPermutations.Add(new Suit
                {
                    LostResistancePoints = i * 2,
                    NumberOfImbues = i + 1,
                    TotalResistances = new Resistances
                    {
                        PhysicalResist = i + 1,
                        FireResist = i + 2,
                        ColdResist = i + 3,
                        PoisonResist = i + 4,
                        EnergyResist = i + 5,
                    }
                });
            }

            SuitPermutations = suitPermutations;
            SelectedSuit = suitBuilder.Build();
            BuffedResistances = SelectedSuit.TotalResistances;
            TargetResists = new Resistances
            {
                PhysicalResist = 85,
                FireResist = 90,
                ColdResist = 60,
                PoisonResist = 60,
                EnergyResist = 65,
            };
            MaxResists = new Resistances
            {
                PhysicalResist = 70,
                FireResist = 70,
                ColdResist = 70,
                PoisonResist = 70,
                EnergyResist = 75,
            };
        }

        public Resistances BuffedResistances
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

        public Resistances MaxResists { get; set; }

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

        public Resistances TargetResists { get; set; }

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