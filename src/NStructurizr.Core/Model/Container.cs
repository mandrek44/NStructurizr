using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Model
{
    public class Container : Element
    {

        private SoftwareSystem parent;
        public String technology { get; set; }

        private ISet<Component> components = new HashSet<Component>();

        public Container()
        {
            addTags(Tags.CONTAINER);
        }

        // TODO: @JsonIgnore
        public SoftwareSystem getParent()
        {
            return parent;
        }

        public void setParent(SoftwareSystem parent)
        {
            this.parent = parent;
        }

        public Component addComponentOfType(String interfaceType, String implementationType, String description, String technology)
        {
            Component component = getModel().addComponentOfType(this, interfaceType, implementationType, description);
            component.technology = (technology);

            return component;
        }

        public Component addComponent(String name, String description)
        {
            return getModel().addComponent(this, name, description);
        }

        public Component addComponent(String name, String description, String technology)
        {
            Component c = getModel().addComponent(this, name, description);
            c.technology = (technology);
            return c;
        }

        public void add(Component component)
        {
            if (getComponentWithName(component.name) == null)
            {
                components.Add(component);
            }
        }

        public ISet<Component> getComponents()
        {
            return components;
        }

        public Component getComponentWithName(String name)
        {
            if (name == null)
            {
                return null;
            }

            Component component = components.FirstOrDefault(c => name.Equals(c.name));

            if (component != null)
            {
                return component;
            }
            else
            {
                return null;
            }
        }

        public Component getComponentOfType(String type)
        {
            if (type == null)
            {
                return null;
            }

            Component component = components.FirstOrDefault(c => type.Equals(c.interfaceType));

            if (component != null)
            {
                return component;
            }

            component = components.FirstOrDefault(c => type.Equals(c.implementationType));
            return component;
        }

        public override ElementType type
        {
            get {return ElementType.Container;}
        }

        public override String getCanonicalName()
        {
            return getParent().getCanonicalName() + CANONICAL_NAME_SEPARATOR + formatForCanonicalName(name);
        }

    }
}