using System;
using System.Collections.Generic;

namespace NStructurizr.Core.Attributes
{
    public interface IComponentsFinder
    {
        IEnumerable<Type> FindComponentTypes(Type[] allTypes);
    }
}