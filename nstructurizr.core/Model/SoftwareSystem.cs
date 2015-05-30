using System;
using System.Collections.Generic;

namespace NStructurizr.Core.Model
{
    public class SoftwareSystem : Element
    {

        private Location _location;

        public Location location
        {
            get { return _location; }
            set
            {
                if (value != null)
                {
                    this._location = value;
                }
                else
                {
                    this._location = Location.Unspecified;
                }
            }
        }

        private ISet<Container> _containers = new HashSet<Container>();

        public ISet<Container> containers
        {
            get { return new HashSet<Container>(_containers);}
        }

        public SoftwareSystem()
        {
            _location = Location.Unspecified;
            addTags(Tags.SOFTWARE_SYSTEM);
        }

        public void add(Container container)
        {
            _containers.Add(container);
        }


        /**
         * Adds a container with the specified name, description and technology
         * (unless one exists with the same name already).
         *
         * @param name              the name of the container (e.g. "Web Application")
         * @param description       a short description/list of responsibilities
         * @param technology        the technoogy choice (e.g. "Spring MVC", "Java EE", etc)
         * @return      the newly created Container instance added to the model (or null)
         */
        public Container addContainer(String name, String description, String technology)
        {
            return getModel().addContainer(this, name, description, technology);
        }

        /**
         * Gets the container with the specified name
         * (or null if it doesn't exist).
         */
        public Container getContainerWithName(String name)
        {
            foreach (Container container in containers)
            {
                if (container.name.Equals(name))
                {
                    return container;
                }
            }

            return null;
        }

        /**
         * Gets the container with the specified ID
         * (or null if it doesn't exist).
         */
        public Container getContainerWithId(String id)
        {
            foreach (Container container in containers)
            {
                if (container.id.Equals(id))
                {
                    return container;
                }
            }

            return null;
        }

        /**
         * Adds a unidirectional relationship between this software system and a person.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "sends e-mail to")
         */
        public Relationship delivers(Person destination, String description)
        {
            Relationship relationship = new Relationship(this, destination, description);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional relationship between this software system and a person.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "sends e-mail to")
         * @param technology    the technology details (e.g. JSON/HTTPS)
         */
        public Relationship delivers(Person destination, String description, String technology)
        {
            Relationship relationship = new Relationship(this, destination, description, technology);
            getModel().addRelationship(relationship);

            return relationship;
        }

        public override ElementType getType()
        {
            return ElementType.SoftwareSystem;
        }

        public override String getCanonicalName()
        {
            return CANONICAL_NAME_SEPARATOR + formatForCanonicalName(name);
        }

    }
}