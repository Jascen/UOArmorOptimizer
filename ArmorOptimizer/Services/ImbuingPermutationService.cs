using ArmorOptimizer.Core.Extensions;
using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using System.Collections.Generic;

namespace ArmorOptimizer.Services
{
    public class ImbuingPermutationService
    {
        private readonly int _maxImbues;
        private readonly int _maxResistBonusValue;
        private readonly ResistConfiguration _maxResists;

        public ImbuingPermutationService(int maxResistBonusValue, int maxImbues)
        {
            _maxResistBonusValue = maxResistBonusValue;
            _maxImbues = maxImbues;
            _maxResists = new ResistConfiguration
            {
                Physical = maxResistBonusValue,
                Fire = maxResistBonusValue,
                Cold = maxResistBonusValue,
                Poison = maxResistBonusValue,
                Energy = maxResistBonusValue,
            };
        }

        public void AddImbuable(Item baseItem, ref List<ImbueableItem> permutations)
        {
            if (_maxImbues < 1) return;

            var currentItem = new ImbueableItem
            {
                UoId = baseItem.UoId,
                LostResistancePoints = baseItem.LostResistancePoints,
                NumberOfImbues = baseItem.NumberOfImbues,
                Resistances = new ResistConfiguration
                {
                    Physical = baseItem.PhysicalResist,
                    Fire = baseItem.FireResist,
                    Cold = baseItem.ColdResist,
                    Poison = baseItem.PoisonResist,
                    Energy = baseItem.EnergyResist,
                },
            };

            var itemMaxResists = baseItem.ArmorType.BaseResist.Clone();
            var bonusResistances = baseItem.Resource.BonusResist;
            itemMaxResists.PlusResists(bonusResistances);
            itemMaxResists.PlusResists(_maxResists);

            var itemEvaluatorService = new ImbueableItemEvaluatorService(currentItem, itemMaxResists, _maxResistBonusValue);
            var physicalImbue = itemEvaluatorService.ImbuePhysical();
            permutations.Add(physicalImbue);
            for (var i = 2; i <= _maxImbues; i++) // TODO: This is artificially limiting the max # of imbues to 2.
            {
                if (physicalImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(physicalImbue, itemMaxResists, _maxResistBonusValue);
                if (secondaryImbue != null)
                {
                    permutations.Add(secondaryImbue);
                }
                physicalImbue = secondaryImbue;
            }

            var fireImbue = itemEvaluatorService.ImbueFire();
            permutations.Add(fireImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (fireImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(fireImbue, itemMaxResists, _maxResistBonusValue);
                if (secondaryImbue != null)
                {
                    permutations.Add(secondaryImbue);
                }
                fireImbue = secondaryImbue;
            }

            var energyImbue = itemEvaluatorService.ImbueEnergy();
            permutations.Add(energyImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (energyImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(energyImbue, itemMaxResists, _maxResistBonusValue);
                if (secondaryImbue != null)
                {
                    permutations.Add(secondaryImbue);
                }
                energyImbue = secondaryImbue;
            }

            var coldImbue = itemEvaluatorService.ImbueCold();
            permutations.Add(coldImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (coldImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(coldImbue, itemMaxResists, _maxResistBonusValue);
                if (secondaryImbue != null)
                {
                    permutations.Add(secondaryImbue);
                }
                coldImbue = secondaryImbue;
            }

            var poisonImbue = itemEvaluatorService.ImbuePoison();
            permutations.Add(poisonImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (poisonImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(poisonImbue, itemMaxResists, _maxResistBonusValue);
                if (secondaryImbue != null)
                {
                    permutations.Add(secondaryImbue);
                }
                poisonImbue = secondaryImbue;
            }
        }

        private ImbueableItem PerformFirstLowestImbue(ImbueableItem basePiece, ResistConfiguration maxResists, int maxResistBonus)
        {
            var armorEvaluatorService = new ImbueableItemEvaluatorService(basePiece, maxResists, maxResistBonus);
            if (armorEvaluatorService.PhysicalLowest) return armorEvaluatorService.ImbuePhysical();
            if (armorEvaluatorService.FireLowest) return armorEvaluatorService.ImbueFire();
            if (armorEvaluatorService.EnergyLowest) return armorEvaluatorService.ImbueEnergy();
            if (armorEvaluatorService.ColdLowest) return armorEvaluatorService.ImbueCold();
            if (armorEvaluatorService.PoisonLowest) return armorEvaluatorService.ImbuePoison();

            return null;
        }
    }
}