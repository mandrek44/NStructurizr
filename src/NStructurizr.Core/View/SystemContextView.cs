using System;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class SystemContextView : View {

        public SystemContextView() : this(null, string.Empty)
        {
        }

        /**
     * Creates a system context view for the given software system.
     *
     * @param softwareSystem        the SoftwareSystem to create a view for
     */
        public SystemContextView(SoftwareSystem softwareSystem) :
            this(softwareSystem, null) {
            }

        /**
     * Creates a system context view for the given software system.
     *
     * @param softwareSystem        the SoftwareSystem to create a view for
     * @param description           the (optional) description for the view
     */
        public SystemContextView(SoftwareSystem softwareSystem, String description) :base(softwareSystem, description) {

            addElement(softwareSystem);
        }

        public override ViewType type {
            get { return ViewType.SystemContext; }
        }

        public override String name {
            get { return getSoftwareSystem().name + " - System Context"; }
        }

        /**
     * Adds all software systems and all people to this view.
     */
        public override void addAllElements() {
            AddAllSoftwareSystems();
            AddAllPeople();
        }

    }
}