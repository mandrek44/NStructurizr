using System;
using System.Linq;
using System.Reflection;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.Attributes
{
    public class ComponentsConnector
    {
        public static void FillContainerComponents(Container parentElement, Assembly assembly, IComponentsFinder componentsFinder, Func<Type, bool> typePredicate = null)
        {
            typePredicate = typePredicate ?? (type => true);

            var allTypes = assembly.GetTypes().Where(type => typePredicate(type)).ToArray();
            var componentTypes = componentsFinder.FindComponentTypes(allTypes).ToHashSet();
            var components = componentTypes.ToDictionary(type => type, type => CreateComponent(type, parentElement));

            componentTypes
                .SelectMany(type => TypeDependency.FindComponentDependencies(allTypes, type2 => componentTypes.Contains(type2)))
                .Distinct()
                .ForEach(d => components[d.Parent].Uses(components[d.Child], string.Empty));
        }

        private static Component CreateComponent(Type type, Container parentElement)
        {
            var component = parentElement.addComponent(type.Name, string.Empty);
            component.ImplementingType = type;

            return component;
        }
    }
}