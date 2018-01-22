using ArmorOptimizer.Core.Extensions;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using System;

namespace ArmorOptimizer.Services
{
    public class ImbueableItemEvaluatorService
    {
        private readonly ImbueableItem _baseImbueableItem;
        private readonly ResistConfiguration _itemMaximumResistances;
        private readonly int _maxResistBonus;

        public ImbueableItemEvaluatorService(ImbueableItem imbueableItem, ResistConfiguration itemMaximumResistances, int maxResistBonus)
        {
            if (imbueableItem == null) throw new ArgumentNullException(nameof(imbueableItem));
            if (itemMaximumResistances == null) throw new ArgumentNullException(nameof(itemMaximumResistances));

            PhysicalLowest = imbueableItem.Resistances.Physical <= imbueableItem.Resistances.Fire
                          && imbueableItem.Resistances.Physical <= imbueableItem.Resistances.Cold
                          && imbueableItem.Resistances.Physical <= imbueableItem.Resistances.Poison
                          && imbueableItem.Resistances.Physical <= imbueableItem.Resistances.Energy;
            FireLowest = imbueableItem.Resistances.Fire <= imbueableItem.Resistances.Physical
                         && imbueableItem.Resistances.Fire <= imbueableItem.Resistances.Cold
                         && imbueableItem.Resistances.Fire <= imbueableItem.Resistances.Poison
                         && imbueableItem.Resistances.Fire <= imbueableItem.Resistances.Energy;
            ColdLowest = imbueableItem.Resistances.Cold <= imbueableItem.Resistances.Fire
                         && imbueableItem.Resistances.Cold <= imbueableItem.Resistances.Physical
                         && imbueableItem.Resistances.Cold <= imbueableItem.Resistances.Poison
                         && imbueableItem.Resistances.Cold <= imbueableItem.Resistances.Energy;
            PoisonLowest = imbueableItem.Resistances.Poison <= imbueableItem.Resistances.Fire
                         && imbueableItem.Resistances.Poison <= imbueableItem.Resistances.Cold
                         && imbueableItem.Resistances.Poison <= imbueableItem.Resistances.Physical
                         && imbueableItem.Resistances.Poison <= imbueableItem.Resistances.Energy;
            EnergyLowest = imbueableItem.Resistances.Energy <= imbueableItem.Resistances.Fire
                         && imbueableItem.Resistances.Energy <= imbueableItem.Resistances.Cold
                         && imbueableItem.Resistances.Energy <= imbueableItem.Resistances.Poison
                         && imbueableItem.Resistances.Energy <= imbueableItem.Resistances.Physical;
            _baseImbueableItem = imbueableItem;
            _itemMaximumResistances = itemMaximumResistances;
            _maxResistBonus = maxResistBonus;
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }

        public ImbueableItem ImbueCold()
        {
            var armor = _baseImbueableItem.Clone();
            var idealMax = armor.Resistances.Cold + _maxResistBonus;
            if (idealMax < _itemMaximumResistances.Cold) throw new ArgumentException($"'{armor.UoId}' does not have correct base material selected.");

            armor.Resistances.Cold = Math.Min(idealMax, _itemMaximumResistances.Cold);
            armor.LostResistancePoints = idealMax - _itemMaximumResistances.Cold;
            armor.NumberOfImbues++;

            return armor;
        }

        public ImbueableItem ImbueEnergy()
        {
            var armor = _baseImbueableItem.Clone();
            var idealMax = armor.Resistances.Energy + _maxResistBonus;
            if (idealMax < _itemMaximumResistances.Energy) throw new ArgumentException($"'{armor.UoId}' does not have correct base material selected.");

            armor.Resistances.Energy = Math.Min(idealMax, _itemMaximumResistances.Energy);
            armor.LostResistancePoints = idealMax - _itemMaximumResistances.Energy;
            armor.NumberOfImbues++;

            return armor;
        }

        public ImbueableItem ImbueFire()
        {
            var armor = _baseImbueableItem.Clone();
            var idealMax = armor.Resistances.Fire + _maxResistBonus;
            if (idealMax < _itemMaximumResistances.Fire) throw new ArgumentException($"'{armor.UoId}' does not have correct base material selected.");

            armor.Resistances.Fire = Math.Min(idealMax, _itemMaximumResistances.Fire);
            armor.LostResistancePoints = idealMax - _itemMaximumResistances.Fire;
            armor.NumberOfImbues++;

            return armor;
        }

        public ImbueableItem ImbuePhysical()
        {
            var armor = _baseImbueableItem.Clone();
            var idealMax = armor.Resistances.Physical + _maxResistBonus;
            if (idealMax < _itemMaximumResistances.Physical) throw new ArgumentException($"'{armor.UoId}' does not have correct base material selected.");

            armor.Resistances.Physical = Math.Min(idealMax, _itemMaximumResistances.Physical);
            armor.LostResistancePoints = idealMax - _itemMaximumResistances.Physical;
            armor.NumberOfImbues++;

            return armor;
        }

        public ImbueableItem ImbuePoison()
        {
            var armor = _baseImbueableItem.Clone();
            var idealMax = armor.Resistances.Poison + _maxResistBonus;
            if (idealMax < _itemMaximumResistances.Poison) throw new ArgumentException($"'{armor.UoId}' does not have correct base material selected.");

            armor.Resistances.Poison = Math.Min(idealMax, _itemMaximumResistances.Poison);
            armor.LostResistancePoints = idealMax - _itemMaximumResistances.Poison;
            armor.NumberOfImbues++;

            return armor;
        }
    }
}