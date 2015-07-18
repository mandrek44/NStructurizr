using System;
using System.Linq;
using System.Reflection;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.Attributes
{
    public class ComponentRelationshipsFinder
    {
        public static Relationship[] FindContainerRelationships(Container parentElement, IComponentsFinder componentsFinder, Func<Type, bool> assemblyTypeSelector, params Assembly[] assemblies)
        {
            assemblyTypeSelector = assemblyTypeSelector ?? (type => true);
            var allTypes = assemblies.SelectMany(a => a.GetTypes()).Where(type => assemblyTypeSelector(type)).ToArray();

            return FindContainerRelationships(parentElement, componentsFinder, allTypes);
        }

        public static Relationship[] FindContainerRelationships(Container parentElement, IComponentsFinder componentsFinder, Type[] allTypes)
        {
            var componentTypes = componentsFinder.FindComponentTypes(allTypes).ToHashSet();
            var components = componentTypes.ToDictionary(type => type, type => CreateComponent(type, parentElement));

            return componentTypes
                .SelectMany(type => TypeDependency.FindComponentDependencies(allTypes, type2 => componentTypes.Contains(type2)))
                .Distinct()
                .Select(d => components[d.Parent].Uses(components[d.Child], string.Empty))
                .ToArray();
        }

        private static Component CreateComponent(Type type, Container parentElement)
        {
            var component = parentElement.addComponent(type.Name, string.Empty);
            component.ImplementingType = type;

            return component;
        }
    }
}