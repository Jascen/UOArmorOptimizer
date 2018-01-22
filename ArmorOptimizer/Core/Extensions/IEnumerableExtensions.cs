using System.Collections.Generic;
using System.Linq;

namespace ArmorOptimizer.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static List<T> ToEnumeratedList<T>(this IEnumerable<T> enumerable)
        {
            return enumerable == null ? new List<T>() : enumerable as List<T> ?? enumerable.ToList();
        }
    }
}