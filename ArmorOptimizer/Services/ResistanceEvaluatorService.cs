using ArmorOptimizer.Core.Extensions;
using ArmorOptimizer.EntityFramework;
using System;

namespace ArmorOptimizer.Services
{
    public class ResistanceEvaluatorService
    {
        public ResistanceEvaluatorService(ResistConfiguration currentResists, ResistConfiguration targetResists)
        {
            if (currentResists == null) throw new ArgumentNullException(nameof(currentResists));
            if (targetResists == null) throw new ArgumentNullException(nameof(targetResists));

            var resistDeficits = targetResists.Clone();
            resistDeficits.MinusResists(currentResists, true);

            PhysicalLowest = resistDeficits.Physical >= resistDeficits.Fire && resistDeficits.Physical >= resistDeficits.Cold && resistDeficits.Physical >= resistDeficits.Poison && resistDeficits.Physical >= resistDeficits.Energy;
            FireLowest = resistDeficits.Fire >= resistDeficits.Physical && resistDeficits.Fire >= resistDeficits.Cold && resistDeficits.Fire >= resistDeficits.Poison && resistDeficits.Fire >= resistDeficits.Energy;
            ColdLowest = resistDeficits.Cold >= resistDeficits.Fire && resistDeficits.Cold >= resistDeficits.Physical && resistDeficits.Cold >= resistDeficits.Poison && resistDeficits.Cold >= resistDeficits.Energy;
            PoisonLowest = resistDeficits.Poison >= resistDeficits.Fire && resistDeficits.Poison >= resistDeficits.Cold && resistDeficits.Poison >= resistDeficits.Physical && resistDeficits.Poison >= resistDeficits.Energy;
            EnergyLowest = resistDeficits.Energy >= resistDeficits.Fire && resistDeficits.Energy >= resistDeficits.Cold && resistDeficits.Energy >= resistDeficits.Poison && resistDeficits.Energy >= resistDeficits.Physical;
        }

        public bool ColdLowest { get; }
        public bool EnergyLowest { get; }
        public bool FireLowest { get; }
        public bool PhysicalLowest { get; }
        public bool PoisonLowest { get; }
    }
}