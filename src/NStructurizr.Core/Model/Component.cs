using System;

namespace NStructurizr.Core.Model
{
    public class Component : Element
    {

        private Container parent;

        public String technology { get; set; }
        public String interfaceType { get; set; }
        public String implementationType { get; set; }
        public String sourcePath { get; set; }

        public Component()
        {
            addTags(Tags.COMPONENT);
        }

        // TODO: @JsonIgnore
        public Container getParent()
        {
            return parent;
        }

        public void setParent(Container parent)
        {
            this.parent = parent;
        }

        public override String name
        {
            get
            {
                if (this.name != null)
                {
                    return base.name;
                }
                else if (this.interfaceType != null)
                {
                    return interfaceType.Substring(interfaceType.LastIndexOf(".") + 1);
                }
                else if (this.implementationType != null)
                {
                    return implementationType.Substring(implementationType.LastIndexOf(".") + 1);
                }
                else
                {
                    return "";
                }
            }
            set { base.name = value; }
        }

        //TODO: @JsonIgnore
        public String getPackage()
        {
            return interfaceType.Substring(0, interfaceType.LastIndexOf("."));
        }

        public override ElementType type
        {
            get { return ElementType.Component; }
        }

        public override String getCanonicalName()
        {
            return getParent().getCanonicalName() + CANONICAL_NAME_SEPARATOR + formatForCanonicalName(name);
        }

    }
}