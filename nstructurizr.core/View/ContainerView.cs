using System;
using System.Linq;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ContainerView : View {

        //ContainerView() {
        //}

        public ContainerView(SoftwareSystem softwareSystem, String description) : base(softwareSystem, description) {
        }

        /**
     * Adds all software systems in the model to this view.
     */
        public override void addAllSoftwareSystems() {
            getModel().getSoftwareSystems()
                .Where(ss => ss != getSoftwareSystem())
                .ForEach(addElement);
        }

        /**
     * Adds all containers in the software system to this view.
     */
        public void addAllContainers() {
            getSoftwareSystem().getContainers().ForEach(addElement);
        }

        public override ViewType getType() {
            return ViewType.Container;
        }

        public override String getName() {
            return getSoftwareSystem().getName() + " - Containers";
        }

        public override void addAllElements() {
            addAllSoftwareSystems();
            addAllPeople();
            addAllContainers();
        }

    }
}