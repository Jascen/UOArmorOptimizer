using ArmorOptimizer.Annotations;
using ArmorOptimizer.Builders;
using ArmorOptimizer.EntityFramework;
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
        private ResistConfiguration _buffedResistances;
        private Suit _selectedSuit;
        private IEnumerable<Suit> _suitPermutations;

        public MainWindowViewModel()
        {
            Service = new MainWindowService(this);

            var suitBuilder = new SuitBuilder();
            var sampleResists = new ResistConfiguration
            {
                Physical = 11,
                Fire = 12,
                Cold = 13,
                Poison = 14,
                Energy = 15,
            };
            suitBuilder.AddHelm(CheaterMethod(SlotTypes.Helm, sampleResists, "Verite"));
            suitBuilder.AddChest(CheaterMethod(SlotTypes.Chest, sampleResists, "Verite"));
            suitBuilder.AddArms(CheaterMethod(SlotTypes.Arms, sampleResists, "Verite"));
            suitBuilder.AddGloves(CheaterMethod(SlotTypes.Gloves, sampleResists, "Barbed"));
            suitBuilder.AddLegs(CheaterMethod(SlotTypes.Legs, sampleResists, "Verite"));
            var suitPermutations = new List<Suit>();
            for (var i = 0; i < 100; i++)
            {
                suitPermutations.Add(new Suit
                {
                    LostResistancePoints = i * 2,
                    NumberOfImbues = i + 1,
                    TotalResistances = new ResistConfiguration
                    {
                        Physical = i + 1,
                        Fire = i + 2,
                        Cold = i + 3,
                        Poison = i + 4,
                        Energy = i + 5,
                    }
                });
            }

            SuitPermutations = suitPermutations;
            SelectedSuit = suitBuilder.Build();
            BuffedResistances = SelectedSuit.TotalResistances;
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

        protected Item CheaterMethod(SlotTypes slot, ResistConfiguration resists, string resourceName)
        {
            return new Item
            {
                UoId = "XXXXXX",
                ArmorType = new ArmorType
                {
                    Slot = (long)slot
                },
                PhysicalResist = resists.Physical,
                FireResist = resists.Fire,
                ColdResist = resists.Cold,
                PoisonResist = resists.Poison,
                EnergyResist = resists.Energy,
                Resource = new Resource { Name = resourceName }
            };
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