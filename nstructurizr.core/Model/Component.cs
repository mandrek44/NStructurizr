using System;

namespace NStructurizr.Core.Model
{
    public class Component : Element
    {

        private Container parent;

        private String technology = "";
        private String interfaceType;
        private String implementationType;
        private String sourcePath;

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

        public String getTechnology()
        {
            return technology;
        }

        public void setTechnology(String technology)
        {
            this.technology = technology;
        }

        public String getInterfaceType()
        {
            return interfaceType;
        }

        public void setInterfaceType(String interfaceType)
        {
            this.interfaceType = interfaceType;
        }

        public String getImplementationType()
        {
            return implementationType;
        }

        public void setImplementationType(String implementationType)
        {
            this.implementationType = implementationType;
        }

        public String getSourcePath()
        {
            return sourcePath;
        }

        public void setSourcePath(String sourcePath)
        {
            this.sourcePath = sourcePath;
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

        public override ElementType getType()
        {
            return ElementType.Component;
        }

        public override String getCanonicalName()
        {
            return getParent().getCanonicalName() + CANONICAL_NAME_SEPARATOR + formatForCanonicalName(name);
        }

    }
}