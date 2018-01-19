using System;

namespace ArmorOptimizer.Core.Attributes
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