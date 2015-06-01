using System;
using System.Linq;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    // TODO: Fix serialization in .NET
    public class ComponentView : View
    {
        private Container container;
        private String containerId;

        public ComponentView(Container container, String description)
            : base(container.getParent(), description)
        {

            this.container = container;
        }

        public String getContainerId()
        {
            if (this.container != null)
            {
                return container.id;
            }
            else
            {
                return this.containerId;
            }
        }

        void setContainerId(String containerId)
        {
            this.containerId = containerId;
        }

        // TODO: @JsonIgnore
        public Container getContainer()
        {
            return container;
        }

        public void setContainer(Container container)
        {
            this.container = container;
        }

        /**
     * Adds all software systems in the model to this view.
     */
        public override void addAllSoftwareSystems()
        {
            getModel().softwareSystems
                .Where(ss => ss != getSoftwareSystem())
                .ForEach(addElement);
        }

        /**
     * Adds all containers in the software system to this view.
     */
        public void addAllContainers()
        {
            getSoftwareSystem().containers
                .Where(c => c != container)
                .ForEach(addElement);
        }

        /**
     * Adds all components in the container to this view.
     */
        public void addAllComponents()
        {
            container.getComponents().ForEach(addElement);
        }

        /**
     * Adds an individual container to this view.
     *
     * @param container     the Container to add
     */
        public void add(Container container)
        {
            addElement(container);
        }

        /**
     * Adds an individual component to this view.
     *
     * @param component     the Component to add
     */
        public void add(Component component)
        {
            addElement(component);
        }

        /**
     * Removes an individual container from this view.
     *
     * @param container     the Container to remove
     */
        public void remove(Container container)
        {
            removeElement(container);
        }

        /**
     * Removes an individual component from this view.
     *
     * @param component     the Component to remove
     */
        public void remove(Component component)
        {
            removeElement(component);
        }

        public override ViewType type
        {
            get { return ViewType.Component; }
        }

        public override String name
        {
            get { return getSoftwareSystem().name + " - " + getContainer().name + " - Components"; }
        }

        public override void addAllElements()
        {
            addAllSoftwareSystems();
            addAllPeople();
            addAllContainers();
            addAllComponents();
            //        removeElementsThatCantBeReachedFrom(this.container);
        }

    }
}