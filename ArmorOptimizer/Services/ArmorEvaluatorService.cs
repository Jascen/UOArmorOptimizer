using ArmorOptimizer.Core.Extensions;
using ArmorOptimizer.EntityFramework;
using System;

namespace ArmorOptimizer.Services
{
    public class ArmorEvaluatorService
    {
        private readonly Item _baseItem;
        private readonly ResistConfiguration _maxImbueables;
        private readonly int _maxResistBonus;

        public ArmorEvaluatorService(Item armorModel, int maxResistBonus)
        {
            if (armorModel == null) throw new ArgumentNullException(nameof(armorModel));

            PhysicalLowest = armorModel.PhysicalResist <= armorModel.FireResist
                          && armorModel.PhysicalResist <= armorModel.ColdResist
                          && armorModel.PhysicalResist <= armorModel.PoisonResist
                          && armorModel.PhysicalResist <= armorModel.EnergyResist;
            FireLowest = armorModel.FireResist <= armorModel.PhysicalResist
                         && armorModel.FireResist <= armorModel.ColdResist
                         && armorModel.FireResist <= armorModel.PoisonResist
                         && armorModel.FireResist <= armorModel.EnergyResist;
            ColdLowest = armorModel.ColdResist <= armorModel.FireResist
                         && armorModel.ColdResist <= armorModel.PhysicalResist
                         && armorModel.ColdResist <= armorModel.PoisonResist
                         && armorModel.ColdResist <= armorModel.EnergyResist;
            PoisonLowest = armorModel.PoisonResist <= armorModel.FireResist
                         && armorModel.PoisonResist <= armorModel.ColdResist
                         && armorModel.PoisonResist <= armorModel.PhysicalResist
                         && armorModel.PoisonResist <= armorModel.EnergyResist;
            EnergyLowest = armorModel.EnergyResist <= armorModel.FireResist
                         && armorModel.EnergyResist <= armorModel.ColdResist
                         && armorModel.EnergyResist <= armorModel.PoisonResist
                         && armorModel.EnergyResist <= armorModel.PhysicalResist;
            _baseItem = armorModel;
            _maxResistBonus = maxResistBonus;
            _maxImbueables = new ResistConfiguration
            {
                Physical = armorModel.ArmorType.BaseResist.Physical + maxResistBonus,
                Fire = armorModel.ArmorType.BaseResist.Fire + maxResistBonus,
                Cold = armorModel.ArmorType.BaseResist.Cold + maxResistBonus,
                Poison = armorModel.ArmorType.BaseResist.Poison + maxResistBonus,
                Energy = armorModel.ArmorType.BaseResist.Energy + maxResistBonus,
            };
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }

        public Item ImbueCold()
        {
            var armor = _baseItem.Clone();
            var idealMax = armor.ColdResist + _maxResistBonus;
            if (idealMax < _maxImbueables.Cold) throw new ArgumentException($"{armor.ArmorType.SlotType} '{armor.UoId}' does not have correct base material selected.");

            armor.ColdResist = Math.Min(idealMax, _maxImbueables.Cold);
            armor.LostResistancePoints = idealMax - _maxImbueables.Cold;
            armor.NumberOfImbues++;

            return armor;
        }

        public Item ImbueEnergy()
        {
            var armor = _baseItem.Clone();
            var idealMax = armor.EnergyResist + _maxResistBonus;
            if (idealMax < _maxImbueables.Energy) throw new ArgumentException($"{armor.ArmorType.SlotType} '{armor.UoId}' does not have correct base material selected.");

            armor.EnergyResist = Math.Min(idealMax, _maxImbueables.Energy);
            armor.LostResistancePoints = idealMax - _maxImbueables.Energy;
            armor.NumberOfImbues++;

            return armor;
        }

        public Item ImbueFire()
        {
            var armor = _baseItem.Clone();
            var idealMax = armor.FireResist + _maxResistBonus;
            if (idealMax < _maxImbueables.Fire) throw new ArgumentException($"{armor.ArmorType.SlotType} '{armor.UoId}' does not have correct base material selected.");

            armor.FireResist = Math.Min(idealMax, _maxImbueables.Fire);
            armor.LostResistancePoints = idealMax - _maxImbueables.Fire;
            armor.NumberOfImbues++;

            return armor;
        }

        public Item ImbuePhysical()
        {
            var armor = _baseItem.Clone();
            var idealMax = armor.PhysicalResist + _maxResistBonus;
            if (idealMax < _maxImbueables.Physical) throw new ArgumentException($"{armor.ArmorType.SlotType} '{armor.UoId}' does not have correct base material selected.");

            armor.PhysicalResist = Math.Min(idealMax, _maxImbueables.Physical);
            armor.LostResistancePoints = idealMax - _maxImbueables.Physical;
            armor.NumberOfImbues++;

            return armor;
        }

        public Item ImbuePoison()
        {
            var armor = _baseItem.Clone();
            var idealMax = armor.PoisonResist + _maxResistBonus;
            if (idealMax < _maxImbueables.Poison) throw new ArgumentException($"{armor.ArmorType.SlotType} '{armor.UoId}' does not have correct base material selected.");

            armor.PoisonResist = Math.Min(idealMax, _maxImbueables.Poison);
            armor.LostResistancePoints = idealMax - _maxImbueables.Poison;
            armor.NumberOfImbues++;

            return armor;
        }
    }
}