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
                .SelectMany(type => FindTypeFieldDependencies(type, componentTypes))
                .Distinct()
                .ForEach(d => components[d.Parent].Uses(components[d.Child], string.Empty));
        }

        private static IEnumerable<Type> FindComponentTypes(Assembly assembly, Func<Type, bool> typePredicate)
        {
            return assembly.GetTypes()
                .Where(typePredicate)
                .Where(IsComponentType);
        }

        private static IEnumerable<TypeDependency> FindTypeFieldDependencies(Type parent, ICollection<Type> allComponentTypes)
        {
            return parent.GetProperties()
                .Where(property => allComponentTypes.Contains(property.PropertyType))
                .Select(property => new TypeDependency {Parent = parent, Child = property.PropertyType});
        }

        private static Component CreateComponent(Type type, Container parentElement)
        {
            var component = parentElement.addComponent(type.Name, string.Empty);
            component.ImplementingType = type;

            return component;
        }

        private static bool IsComponentType(Type type)
        {
            return type.CustomAttributes.Any(a => a.AttributeType == typeof(ComponentAttribute));
        }

        private struct TypeDependency
        {
            public Type Parent { get; set; }

            public Type Child { get; set; }

            public bool Equals(TypeDependency other)
            {
                return Parent == other.Parent && Child == other.Child;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is TypeDependency && Equals((TypeDependency) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((Parent != null ? Parent.GetHashCode() : 0)*397) ^ (Child != null ? Child.GetHashCode() : 0);
                }
            }

            public override string ToString()
            {
                return string.Format("Parent: {0}, Child: {1}", Parent, Child);
            }
        }
    }
}