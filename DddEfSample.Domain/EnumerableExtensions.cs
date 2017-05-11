using System.Collections.Generic;
using System.Linq;

namespace DddEfSample.Domain
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T item)
        {
            return items.Concat(new[] { item }); 
        }
    }
}
