using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Attributes
{
    public class AttributeComponentsFinder : IComponentsFinder
    {
        public IEnumerable<Type> FindComponentTypes(Type[] allTypes)
        {
            return allTypes.Where(type => type.CustomAttributes.Any(a => a.AttributeType == typeof(ComponentAttribute)));
        }
    }
}
