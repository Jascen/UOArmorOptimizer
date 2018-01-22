using ArmorOptimizer.Core.Attributes;

namespace ArmorOptimizer.Models
{
    public class EasyUoRecord
    {
        [ColumnNumber(6)]
        public int Cold { get; set; }

        [ColumnNumber(3)]
        public int Color { get; set; }

        [ColumnNumber(8)]
        public int Energy { get; set; }

        [ColumnNumber(5)]
        public int Fire { get; set; }

        [ColumnNumber(0)]
        public string Id { get; set; }

        [ColumnNumber(2)]
        public string ItemType { get; set; }

        [ColumnNumber(4)]
        public int Physical { get; set; }

        [ColumnNumber(7)]
        public int Poison { get; set; }

        [ColumnNumber(1)]
        public int Slot { get; set; }
    }
}