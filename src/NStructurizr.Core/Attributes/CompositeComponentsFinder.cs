using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Attributes
{
    public class CompositeComponentsFinder : IComponentsFinder
    {
        private readonly IComponentsFinder[] _finders;

        public CompositeComponentsFinder(params IComponentsFinder[] finders)
        {
            _finders = finders;
        }

        public IEnumerable<Type> FindComponentTypes(Type[] allTypes)
        {
            return _finders.SelectMany(finder => finder.FindComponentTypes(allTypes));
        }
    }
}