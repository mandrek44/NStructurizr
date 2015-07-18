using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Attributes
{
    public class ClassNameComponentsFinder : IComponentsFinder
    {
        public IEnumerable<Type> FindComponentTypes(Type[] allTypes)
        {
            return allTypes.Where(type => type.Name.EndsWith("Component"));
        }
    }
}