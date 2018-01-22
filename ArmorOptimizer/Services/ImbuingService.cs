using ArmorOptimizer.EntityFramework;
using System.Collections.Generic;

namespace ArmorOptimizer.Services
{
    public class ImbuingService
    {
        private readonly int _maxImbues;
        private readonly int _maxResistBonus;

        public ImbuingService(int maxResistBonus, int maxImbues)
        {
            _maxResistBonus = maxResistBonus;
            _maxImbues = maxImbues;
        }

        public void AddImbuable(Item armorModelPiece, ref List<Item> armorPieces)
        {
            if (_maxImbues < 1) return;

            var armorEvaluatorService = new ArmorEvaluatorService(armorModelPiece, _maxResistBonus);
            var physicalImbue = armorEvaluatorService.ImbuePhysical();
            armorPieces.Add(physicalImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (physicalImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(physicalImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                physicalImbue = secondaryImbue;
            }

            var fireImbue = armorEvaluatorService.ImbueFire();
            armorPieces.Add(fireImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (fireImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(fireImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                fireImbue = secondaryImbue;
            }

            var energyImbue = armorEvaluatorService.ImbueEnergy();
            armorPieces.Add(energyImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (energyImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(energyImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                energyImbue = secondaryImbue;
            }

            var coldImbue = armorEvaluatorService.ImbueCold();
            armorPieces.Add(coldImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (coldImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(coldImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                coldImbue = secondaryImbue;
            }

            var poisonImbue = armorEvaluatorService.ImbuePoison();
            armorPieces.Add(poisonImbue);
            for (var i = 2; i <= _maxImbues; i++)
            {
                if (poisonImbue == null) break;

                var secondaryImbue = PerformFirstLowestImbue(poisonImbue, _maxResistBonus);
                if (secondaryImbue != null)
                {
                    armorPieces.Add(secondaryImbue);
                }
                poisonImbue = secondaryImbue;
            }
        }

        private Item PerformFirstLowestImbue(Item basePiece, int maxResistBonus)
        {
            var armorEvaluatorService = new ArmorEvaluatorService(basePiece, maxResistBonus);
            if (armorEvaluatorService.PhysicalLowest) return armorEvaluatorService.ImbuePhysical();
            if (armorEvaluatorService.FireLowest) return armorEvaluatorService.ImbueFire();
            if (armorEvaluatorService.EnergyLowest) return armorEvaluatorService.ImbueEnergy();
            if (armorEvaluatorService.ColdLowest) return armorEvaluatorService.ImbueCold();
            if (armorEvaluatorService.PoisonLowest) return armorEvaluatorService.ImbuePoison();

            return null;
        }
    }
}