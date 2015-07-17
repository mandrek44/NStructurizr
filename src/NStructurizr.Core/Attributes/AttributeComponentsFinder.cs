using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.Attributes
{
    public class AttributeComponentsFinder
    {
        public static void FillContainerComponents(Container parentElement, Assembly assembly, Func<Type, bool> typePredicate = null)
        {
            typePredicate = typePredicate ?? (type => true);

            var componentTypes = FindComponentTypes(assembly, typePredicate).ToHashSet();
            var components = componentTypes.ToDictionary(type => type, type => CreateComponent(type, parentElement));

            componentTypes
                .SelectMany(type => TypeDependency.FindComponentDependencies(assembly.GetTypes(), type2 => componentTypes.Contains(type2)))
                .Distinct()
                .ForEach(d => components[d.Parent].Uses(components[d.Child], string.Empty));
        }

        private static IEnumerable<Type> FindComponentTypes(Assembly assembly, Func<Type, bool> typePredicate)
        {
            return assembly.GetTypes()
                .Where(typePredicate)
                .Where(type => type.CustomAttributes.Any(a => a.AttributeType == typeof(ComponentAttribute)));
        }

        private static Component CreateComponent(Type type, Container parentElement)
        {
            var component = parentElement.addComponent(type.Name, string.Empty);
            component.ImplementingType = type;

            return component;
        }
    }
}
