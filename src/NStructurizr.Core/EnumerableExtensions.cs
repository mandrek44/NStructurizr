using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(
            this IEnumerable<T> source,
            Action<T> action)
        {
            foreach (T element in source)
                action(element);
        }

        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source)
        {
            return new HashSet<T>(source);
        }

        public static void RemoveIf<T>(this ISet<T> source,
            Func<T, bool> action)
        {
            var toRemove = source.Where(action).ToArray();
            toRemove.ForEach(tr => source.Remove(tr));
        }
    }
}