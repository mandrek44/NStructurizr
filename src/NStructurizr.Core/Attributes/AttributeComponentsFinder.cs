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
            var components = FindComponents(parentElement, assembly, typePredicate);
            var dependencies = components.SelectMany(component => FindComponentDependencies(component, components));

            foreach (var dependency in dependencies)
                dependency.Parent.Uses(dependency.Child, string.Empty);
        }

        private static Component[] FindComponents(Container parentElement, Assembly assembly, Func<Type, bool> typePredicate = null)
        {
            typePredicate = typePredicate ?? (type => true);

            return assembly.GetTypes()
                .Where(typePredicate)
                .Where(IsComponentType)
                .Select(type => CreateComponent(type, parentElement))
                .ToArray();
        }

        private static IEnumerable<ComponentDependency> FindComponentDependencies(Component parent, IEnumerable<Component> allComponents)
        {
            return parent.GetType().GetFields()
                .Where(field => IsComponentType(field.FieldType))
                .Select(field => CreateComponent(field.FieldType, parent.getParent()))
                .Select(temporaryComponent => allComponents.FirstOrDefault(component => component.Equals(temporaryComponent)))
                .Where(component => component != null)
                .Select(component => new ComponentDependency { Parent = parent, Child = component });
        }

        private static Component CreateComponent(Type type, Container parentElement)
        {
            return parentElement.addComponent(type.Name, string.Empty);
        }

        private static bool IsComponentType(Type type)
        {
            return type.CustomAttributes.Any(a => a.AttributeType == typeof(ComponentAttribute));
        }

        private sealed class ComponentDependency
        {
            public Component Parent { get; set; }

            public Component Child { get; set; }
        }
    }
}