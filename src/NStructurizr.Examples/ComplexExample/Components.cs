using NStructurizr.Core.Attributes;

namespace NStructurizr.Examples.ComplexExample
{
    [Component]
    public class ComponentA
    {
        public ComponentB DependencyToB { get; set; } 
    }

    [Component]
    public class ComponentB
    {
        
    }
}