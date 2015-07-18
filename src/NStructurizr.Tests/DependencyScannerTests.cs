using System;
using System.Linq;
using NStructurizr.Core.Attributes;
using NStructurizr.Core.Model;
using NUnit.Framework;

namespace NStructurizr.Tests
{
    public partial class DependencyScannerTests
    {
        public class DependencyTestsBase
        {
            protected Container TestContainer;
            protected Type TestClassType;

            [SetUp]
            public void ScanForComponents()
            {
                // given
                TestContainer = new Model().AddSoftwareSystem("testSystem", string.Empty).addContainer("testContainer", string.Empty, string.Empty);
                TestClassType = GetType();

                // when
                ComponentRelationshipsFinder.FindContainerRelationships(TestContainer, new AttributeComponentsFinder(), type => type.FullName.Contains(TestClassType.FullName), TestClassType.Assembly);
            }
        }        

        public class GivenTwoComponents : DependencyTestsBase
        {
            [Test]
            public void ShouldReturnAllComponents()
            {
                var componentNames = TestContainer.components.Select(component => component.getCanonicalName()).ToArray();
                CollectionAssert.AreEquivalent(new[] {"/testSystem/testContainer/ComponentA", "/testSystem/testContainer/ComponentB"}, componentNames);
            }

            [Component]
            public class ComponentA
            {
            }

            [Component]
            public class ComponentB
            {
            }
        }

        [TestFixture]
        public class PropertyDependencies
        {
            public class GivenPropertyDependency : DependencyTestsBase
            {
                [Test]
                public void ShouldFindComponentDependency()
                {
                    // then
                    var componentA = TestContainer.components.First(component => component.getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    var relationship = componentA.relationships.Single();

                    Assert.That(relationship.getSource().getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    Assert.That(relationship.getDestination().getCanonicalName() == "/testSystem/testContainer/ComponentB");
                }

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

            public class GivenDoublePropertyDependency : DependencyTestsBase
            {
                [Test]
                public void ShouldFindSingleComponentDependency()
                {
                    // then
                    var componentA = TestContainer.components.First(component => component.getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    var relationship = componentA.relationships.Single();

                    Assert.That(relationship.getSource().getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    Assert.That(relationship.getDestination().getCanonicalName() == "/testSystem/testContainer/ComponentB");
                }

                [Component]
                public class ComponentA
                {
                    public ComponentB FirstDependencyToB { get; set; }

                    public ComponentB SecondDependencyToB { get; set; }
                }

                [Component]
                public class ComponentB
                {
                }
            }

            public class GivenIndirectDegreeDependency : DependencyTestsBase
            {
                [Test]
                public void ShouldFindSingleComponentDependency()
                {
                    // then
                    var componentA = TestContainer.components.First(component => component.getCanonicalName() == "/testSystem/testContainer/ComponentA");

                    // ComponentC is not dependency of ComponentA
                    var relationship = componentA.relationships.Single();

                    Assert.That(relationship.getSource().getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    Assert.That(relationship.getDestination().getCanonicalName() == "/testSystem/testContainer/ComponentB");
                }

                [Component]
                public class ComponentA
                {
                    public SomeClass DependencyToSomeClass { get; set; }
                }

                public class SomeClass
                {
                    public ComponentB DependencyToB { get; set; }
                }

                [Component]
                public class ComponentB
                {
                    public ComponentC DependencyToC { get; set; }
                }

                [Component]
                public class ComponentC
                {
                }
            }
        }

        [TestFixture]
        public class InterfaceDependencies
        {

            public class GivenIndirectInterfaceDependency : DependencyTestsBase
            {
                [Test]
                public void ShouldFindSingleComponentDependency()
                {
                    // then
                    var componentA = TestContainer.components.First(component => component.getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    var relationship = componentA.relationships.Single();

                    Assert.That(relationship.getSource().getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    Assert.That(relationship.getDestination().getCanonicalName() == "/testSystem/testContainer/ComponentB");
                }

                [Component]
                public class ComponentA
                {
                    public ISomeInterface DependencyToSomeInterface { get; set; }
                }

                public interface ISomeInterface
                {
                }

                public class SomeClass : ISomeInterface
                {
                    public ComponentB DependencyToB { get; set; }
                }

                [Component]
                public class ComponentB
                {
                }
            }
        }

        [TestFixture]
        public class ConstructorDependecies
        {

            public class GivenConstructorDependency : DependencyTestsBase
            {
                [Test]
                public void ShouldFindSingleComponentDependency()
                {
                    // then
                    var componentA = TestContainer.components.First(component => component.getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    var relationship = componentA.relationships.Single();

                    Assert.That(relationship.getSource().getCanonicalName() == "/testSystem/testContainer/ComponentA");
                    Assert.That(relationship.getDestination().getCanonicalName() == "/testSystem/testContainer/ComponentB");
                }

                [Component]
                public class ComponentA
                {
                    public ComponentA(ComponentB dependencyToComponentB)
                    {
                    }
                }

                [Component]
                public class ComponentB
                {
                }
            }
        }
    }
}
