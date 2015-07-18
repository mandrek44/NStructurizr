using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Attributes
{
    public class TypeDependency
    {
        public TypeDependency(Type parent, Type child)
        {
            Parent = parent;
            Child = child;
        }

        public Type Parent { get; private set; }

        public Type Child { get; private set; }

        public static IEnumerable<TypeDependency> FindComponentDependencies(Type[] allTypes, Func<Type, bool> isComponent)
        {
            var typeDependencies = allTypes.SelectMany(FindTypeDependencies)
                .Distinct()
                .GroupBy(dependency => dependency.Parent)
                .ToDictionary(group => group.Key, group => group.Select(dependency => dependency.Child).ToArray());

            var componentTypes = allTypes.Where(isComponent).ToHashSet();

            Func<Type, IEnumerable<TypeDependency>> findComponentDependencies = parentComponentType =>
            {
                return Graphs.BreadthFirstSearch(
                    parentComponentType,
                    type => type == parentComponentType || !componentTypes.Contains(type) ? typeDependencies.GetOrDefault(type) : null, // Retrieve dependencies only for non component types (except the parent compontnt type)
                    type => componentTypes.Contains(type))
                    .Select(type => new TypeDependency(parentComponentType, type));
            };

            return componentTypes.SelectMany(findComponentDependencies);
        }

        private static IEnumerable<TypeDependency> FindTypeDependencies(Type type)
        {
            var directlyDependentTypes = new[]
            {
                type.GetFields().Select(field => field.FieldType),
                type.GetProperties().Select(property => property.PropertyType),
                type.GetMethods().SelectMany(method => method.GetParameters().Select(parameter => parameter.ParameterType).Concat(new [] {method.ReturnType})),
                type.GetConstructors().SelectMany(constructor => constructor.GetParameters().Select(parameter => parameter.ParameterType)),
                type.GetInterfaces()
            }
                .SelectMany(x => x)
                .ToArray();

            var genericDependentTypes = directlyDependentTypes.Where(t => type.IsGenericType).SelectMany(t => type.GenericTypeArguments);

            return new[]
            {
                directlyDependentTypes,
                genericDependentTypes
            }.SelectMany(child => child)
                .Select(x => new TypeDependency(type, x))
                .ToArray();
        }

        public bool Equals(TypeDependency other)
        {
            return Parent == other.Parent && Child == other.Child;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TypeDependency && Equals((TypeDependency)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Parent != null ? Parent.GetHashCode() : 0) * 397) ^ (Child != null ? Child.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            return string.Format("Parent: {0}, Child: {1}", Parent, Child);
        }
    }
}