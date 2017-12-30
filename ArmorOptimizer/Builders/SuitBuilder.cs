using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Models;
using System;
using System.Collections.Generic;

namespace ArmorOptimizer.Builders
{
    public class SuitBuilder
    {
        protected ArmorThing Arms;

        protected ArmorThing Chest;

        protected ArmorThing Gloves;

        protected ArmorThing Helm;

        protected ArmorThing Legs;

        protected ArmorThing Misc;

        protected Suit Suit;

        public SuitBuilder AddArms(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Arms) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Arms = armorThing;
            return this;
        }

        public SuitBuilder AddChest(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Chest) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Chest = armorThing;
            return this;
        }

        public SuitBuilder AddGloves(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Gloves) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Gloves = armorThing;
            return this;
        }

        public SuitBuilder AddHelm(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Helm) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Helm = armorThing;
            return this;
        }

        public SuitBuilder AddLegs(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Legs) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Legs = armorThing;
            return this;
        }

        public SuitBuilder AddMisc(ArmorThing armorThing)
        {
            if (armorThing.SlotType != SlotTypes.Misc) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Misc = armorThing;
            return this;
        }

        public Suit Build()
        {
            Suit = new Suit
            {
                TotalResistances = new ResistConfiguration(),
                SuitPieces = new List<ArmorThing>(),
            };
            AddToSuit(Helm);
            AddToSuit(Chest);
            AddToSuit(Arms);
            AddToSuit(Gloves);
            AddToSuit(Legs);
            AddToSuit(Misc);

            return Suit;
        }

        protected void AddToSuit(ArmorThing armorThing)
        {
            if (armorThing == null) return;

            Suit.TotalResistances.Physical += armorThing.Resistances.Physical;
            Suit.TotalResistances.Fire += armorThing.Resistances.Fire;
            Suit.TotalResistances.Cold += armorThing.Resistances.Cold;
            Suit.TotalResistances.Poison += armorThing.Resistances.Poison;
            Suit.TotalResistances.Energy += armorThing.Resistances.Energy;
            Suit.SuitPieces.Add(armorThing);
        }
    }
}