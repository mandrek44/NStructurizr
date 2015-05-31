using System;

namespace NStructurizr.Core.Model
{
    public class Person : Element
    {

        public Location location { get; set; }

        public Person()
        {
            location = Location.Unspecified;
            addTags(Tags.PERSON);
        }

        public override ElementType type
        {
            get { return ElementType.Person; }
        }

        public override String getCanonicalName()
        {
            return CANONICAL_NAME_SEPARATOR + formatForCanonicalName(name);
        }

    }
}