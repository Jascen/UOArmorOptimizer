using System;

namespace ArmorOptimizer.Attributes
{
    public sealed class ColumnNumber : Attribute
    {
        public ColumnNumber(int value)
        {
            Value = value;
        }

        public int Value { get; }
    }
}