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
            getModel().softwareSystems
                .Where(ss => ss != getSoftwareSystem())
                .ForEach(addElement);
        }

        /**
     * Adds all containers in the software system to this view.
     */
        public void addAllContainers() {
            getSoftwareSystem().containers.ForEach(addElement);
        }

        public override ViewType type {
            get { return ViewType.Container; }
        }

        public override String name{
            get { return getSoftwareSystem().name + " - Containers"; }
        }

        public override void addAllElements() {
            addAllSoftwareSystems();
            addAllPeople();
            addAllContainers();
        }

    }
}