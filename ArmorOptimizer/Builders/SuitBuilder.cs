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

        public SuitBuilder Add(Item item)
        {
            if (item == null) return this;

            switch (item.ArmorType.SlotType)
            {
                case SlotTypes.Helm:
                    Helm = item;
                    break;

                case SlotTypes.Chest:
                    Chest = item;
                    break;

                case SlotTypes.Arms:
                    Arms = item;
                    break;

                case SlotTypes.Gloves:
                    Gloves = item;
                    break;

                case SlotTypes.Legs:
                    Legs = item;
                    break;

                case SlotTypes.Misc:
                    Misc = item;
                    break;

                case SlotTypes.Unknown:
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return this;
        }

        public Suit Build()
        {
            Suit = new Suit
            {
                TotalResistances = new ResistConfiguration(),
                Items = new List<Item>(),
                Helm = Helm,
                Chest = Chest,
                Arms = Arms,
                Gloves = Gloves,
                Legs = Legs,
                Misc = Misc,
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
            Suit.LostResistancePoints += (int)item.LostResistancePoints;
            Suit.NumberOfImbues += (int)item.NumberOfImbues;
            Suit.Items.Add(item);
        }
    }
}