using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core
{
    public static class Graphs
    {
        public static IEnumerable<T> BreadthFirstSearch<T>(T origin, Func<T, IEnumerable<T>> getAncestors, Func<T, bool> isGoal)
        {
            var visited = new HashSet<T>(new [] {origin});
            var toVisit = new Stack<T>();
            EnqueueAncestors(getAncestors(origin), visited, toVisit);

            while (toVisit.Any())
            {
                var node = toVisit.Pop();
                if (isGoal(node))
                    yield return node;

                visited.Add(node);
                EnqueueAncestors(getAncestors(node), visited, toVisit);
            }
        }

        private static void EnqueueAncestors<T>(IEnumerable<T> ancestors, HashSet<T> visited, Stack<T> toVisit)
        {
            if (ancestors != null)
                ancestors.Where(ancestor => !visited.Contains(ancestor)).ForEach(toVisit.Push);
        }
    }
}