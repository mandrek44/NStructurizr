using System;

namespace NStructurizr.Core.Model
{
    public class Person : Element
    {

        private Location location = Location.Unspecified;

        public Person()
        {
            addTags(Tags.PERSON);
        }

        public Location getLocation()
        {
            return location;
        }

        public void setLocation(Location location)
        {
            if (location != null)
            {
                this.location = location;
            }
            else
            {
                this.location = Location.Unspecified;
            }
        }

        public override ElementType getType()
        {
            return ElementType.Person;
        }

        public override String getCanonicalName()
        {
            return CANONICAL_NAME_SEPARATOR + formatForCanonicalName(getName());
        }

    }
}