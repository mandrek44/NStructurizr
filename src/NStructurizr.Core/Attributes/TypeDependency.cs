using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Attributes
{
    public class TypeDependency
    {
        private readonly Type _parent;
        private readonly Type _child;

        public Type Parent
        {
            get { return _parent; }
        }

        public Type Child
        {
            get { return _child; }
        }

        public TypeDependency(Type parent, Type child)
        {
            _parent = parent;
            _child = child;
        }

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

        private static IEnumerable<TypeDependency> FindTypeDependencies(Type parent)
        {
            var directlyDependentTypes = parent.GetFields().Select(field => field.FieldType)
                .Concat(parent.GetProperties().Select(property => property.PropertyType))
                .Concat(parent.GetMethods().SelectMany(method => method.GetParameters().Select(parameter => parameter.ParameterType).Concat(new[] {method.ReturnType})))
                .Concat(parent.GetConstructors().SelectMany(constructor => constructor.GetParameters().Select(parameter => parameter.ParameterType)))
                .ToArray();

            var genericDependentTypes = directlyDependentTypes
                .Where(t => parent.IsGenericType)
                .SelectMany(t => parent.GenericTypeArguments);

            var revertedInterfaceDependecies = parent.GetInterfaces().Select(interfaceType => new TypeDependency(interfaceType, parent));

            return directlyDependentTypes
                .Concat(genericDependentTypes)
                .Select(t => new TypeDependency(parent, t))
                .Concat(revertedInterfaceDependecies);
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