using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using ArmorOptimizer.Core.Builders;
using ArmorOptimizer.Core.Enums;

namespace ArmorOptimizer.Services
{
    public class OptimizingService
    {
        private readonly int _maxIterations;

        public OptimizingService(int maxIterations)
        {
            if (maxIterations <= 0) throw new ArgumentOutOfRangeException(nameof(maxIterations));

            _maxIterations = maxIterations;
        }

        public virtual Suit OptimizeSuit(ResistConfiguration targetResists, CategorizedItems categorizedItems, Suit suit, out List<Suit> suitPermutations)
        {
            suitPermutations = new List<Suit>();
            var suitBuilder = new SuitBuilder();
            if (suit != null)
            {
                suitBuilder.Add(suit.Helm)
                    .Add(suit.Chest)
                    .Add(suit.Arms)
                    .Add(suit.Gloves)
                    .Add(suit.Legs)
                    .Add(suit.Misc);
            }
            else
            {
                suitBuilder.Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Helm } })
                    .Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Chest } })
                    .Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Arms } })
                    .Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Gloves } })
                    .Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Legs } })
                    .Add(new Item { ArmorType = new ArmorType { SlotType = SlotTypes.Misc } });
            }

            var iterations = 0;
            var currentSuit = suitBuilder.Build();
            do
            {
                var foundBetterFit = false;
                var betterHelm = FindReplacement(currentSuit.Helm, categorizedItems.Helms, currentSuit.TotalResistances, targetResists);
                if (betterHelm != null)
                {
                    suitBuilder.Add(betterHelm);
                    foundBetterFit = true;
                }

                var betterChest = FindReplacement(currentSuit.Chest, categorizedItems.Chests, currentSuit.TotalResistances, targetResists);
                if (betterChest != null)
                {
                    suitBuilder.Add(betterChest);
                    foundBetterFit = true;
                }

                var betterArms = FindReplacement(currentSuit.Arms, categorizedItems.Arms, currentSuit.TotalResistances, targetResists);
                if (betterArms != null)
                {
                    suitBuilder.Add(betterArms);
                    foundBetterFit = true;
                }

                var betterGloves = FindReplacement(currentSuit.Gloves, categorizedItems.Gloves, currentSuit.TotalResistances, targetResists);
                if (betterGloves != null)
                {
                    suitBuilder.Add(betterGloves);
                    foundBetterFit = true;
                }

                var betterLegs = FindReplacement(currentSuit.Legs, categorizedItems.Legs, currentSuit.TotalResistances, targetResists);
                if (betterLegs != null)
                {
                    suitBuilder.Add(betterLegs);
                    foundBetterFit = true;
                }

                var betterMisc = FindReplacement(currentSuit.Misc, categorizedItems.Misc, currentSuit.TotalResistances, targetResists);
                if (betterMisc != null)
                {
                    suitBuilder.Add(betterMisc);
                    foundBetterFit = true;
                }

                if (!foundBetterFit) break;

                currentSuit = suitBuilder.Build();
                suitPermutations.Add(currentSuit);
            } while (++iterations < _maxIterations);

            return suitPermutations.Count > 0 ? currentSuit : null;
        }

        protected virtual ArmorCandidate ConvertToCandidate(Item currentItem, ResistConfiguration currentResists, ResistConfiguration targetResists, Item candidateItem)
        {
            var armorCandidate = new ArmorCandidate { Item = candidateItem };
            var basePhysical = currentResists.Physical - currentItem.PhysicalResist;
            var newPhysical = basePhysical + candidateItem.PhysicalResist - targetResists.Physical;
            armorCandidate.PhysicalCandidateInfo = FormCandidate(newPhysical);

            var baseFire = currentResists.Fire - currentItem.FireResist;
            var newFire = baseFire + candidateItem.FireResist - targetResists.Fire;
            armorCandidate.FireCandidateInfo = FormCandidate(newFire);

            var baseCold = currentResists.Cold - currentItem.ColdResist;
            var newCold = baseCold + candidateItem.ColdResist - targetResists.Cold;
            armorCandidate.ColdCandidateInfo = FormCandidate(newCold);

            var basePoison = currentResists.Poison - currentItem.PoisonResist;
            var newPoison = basePoison + candidateItem.PoisonResist - targetResists.Poison;
            armorCandidate.PoisonCandidateInfo = FormCandidate(newPoison);

            var baseEnergy = currentResists.Energy - currentItem.EnergyResist;
            var newEnergy = baseEnergy + candidateItem.EnergyResist - targetResists.Energy;
            armorCandidate.EnergyCandidateInfo = FormCandidate(newEnergy);

            return armorCandidate;

            // Local methods start here
            CandidateInfo FormCandidate(long newResist)
            {
                return new CandidateInfo
                {
                    Value = Math.Abs(newResist),
                    OverMax = newResist > 0,
                    UnderMax = newResist < 0,
                };
            }
        }

        protected virtual Item FindReplacement(Item currentItem, IEnumerable<Item> potentialReplacements, ResistConfiguration currentResists, ResistConfiguration targetResists)
        {
            var candidates = potentialReplacements.Select(item => ConvertToCandidate(currentItem, currentResists, targetResists, item)).ToList();
            if (!candidates.Any()) return null;

            var resistanceEvaluatorService = new ResistanceEvaluatorService(currentResists, targetResists);
            var totalLostResistsGroup = candidates.GroupBy(c => c.PhysicalCandidateInfo.Value + c.FireCandidateInfo.Value + c.ColdCandidateInfo.Value + c.PoisonCandidateInfo.Value + c.EnergyCandidateInfo.Value);
            var lowestWastedResists = totalLostResistsGroup.OrderBy(c => c.Key).First();
            foreach (var replacementCandidate in lowestWastedResists)
            {
                if (replacementCandidate.Item == currentItem) continue;

                var armorChanged = false;
                if (resistanceEvaluatorService.PhysicalLowest)
                {
                    armorChanged = replacementCandidate.Item.PhysicalResist > currentItem.PhysicalResist;
                }
                else if (resistanceEvaluatorService.FireLowest)
                {
                    armorChanged = replacementCandidate.Item.FireResist > currentItem.FireResist;
                }
                else if (resistanceEvaluatorService.ColdLowest)
                {
                    armorChanged = replacementCandidate.Item.ColdResist > currentItem.ColdResist;
                }
                else if (resistanceEvaluatorService.PoisonLowest)
                {
                    armorChanged = replacementCandidate.Item.PoisonResist > currentItem.PoisonResist;
                }
                else if (resistanceEvaluatorService.EnergyLowest)
                {
                    armorChanged = replacementCandidate.Item.EnergyResist > currentItem.EnergyResist;
                }

                if (armorChanged) return replacementCandidate.Item;
            }

            return null;
        }
    }
}