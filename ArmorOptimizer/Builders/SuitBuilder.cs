using ArmorOptimizer.EntityFramework;
using ArmorOptimizer.Enums;
using ArmorOptimizer.Models;
using System;
using System.Collections.Generic;

namespace ArmorOptimizer.Builders
{
    public class SuitBuilder
    {
        protected Item Arms;

        protected Item Chest;

        protected Item Gloves;

        protected Item Helm;

        protected Item Legs;

        protected Item Misc;

        protected Suit Suit;

        public SuitBuilder AddArms(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Arms) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Arms = item;
            return this;
        }

        public SuitBuilder AddChest(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Chest) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Chest = item;
            return this;
        }

        public SuitBuilder AddGloves(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Gloves) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Gloves = item;
            return this;
        }

        public SuitBuilder AddHelm(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Helm) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Helm = item;
            return this;
        }

        public SuitBuilder AddLegs(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Legs) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Legs = item;
            return this;
        }

        public SuitBuilder AddMisc(Item item)
        {
            if ((SlotTypes)item.ArmorType.Slot != SlotTypes.Misc) throw new ArgumentException("Only Arms may be assigned to this Property.");

            Misc = item;
            return this;
        }

        public Suit Build()
        {
            Suit = new Suit
            {
                TotalResistances = new ResistConfiguration(),
                SuitPieces = new List<Item>(),
            };
            AddToSuit(Helm);
            AddToSuit(Chest);
            AddToSuit(Arms);
            AddToSuit(Gloves);
            AddToSuit(Legs);
            AddToSuit(Misc);

            return Suit;
        }

        protected void AddToSuit(Item item)
        {
            if (item == null) return;

            Suit.TotalResistances.Physical += item.PhysicalResist;
            Suit.TotalResistances.Fire += item.FireResist;
            Suit.TotalResistances.Cold += item.ColdResist;
            Suit.TotalResistances.Poison += item.PoisonResist;
            Suit.TotalResistances.Energy += item.EnergyResist;
            Suit.SuitPieces.Add(item);
        }
    }
}