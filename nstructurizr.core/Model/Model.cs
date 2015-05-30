using System;
using System.Collections.Generic;
using System.Linq;

namespace NStructurizr.Core.Model
{
    public class Model
    {

        private SequentialIntegerIdGeneratorStrategy idGenerator = new SequentialIntegerIdGeneratorStrategy();

        private readonly Dictionary<string, Element> elementsById = new Dictionary<string, Element>();
        private readonly Dictionary<string, Relationship> relationshipsById = new Dictionary<string, Relationship>();

        private ISet<Person> _people = new HashSet<Person>();

        public ISet<Person> people
        {
            get {  return new HashSet<Person>(_people);}
        }

        private ISet<SoftwareSystem> _softwareSystems = new HashSet<SoftwareSystem>();

        public ISet<SoftwareSystem> softwareSystems
        {
            get {  return new HashSet<SoftwareSystem>(_softwareSystems);}
        }

        public Model()
        {

        }

        /**
         * Creates a software system (location is unspecified) and adds it to the model
         * (unless one exists with the same name already).
         *
         * @param name          the name of the software system
         * @param description   a short description of the software system
         * @return  the SoftwareSystem instance created and added to the model (or null)
         */
        public SoftwareSystem addSoftwareSystem(String name, String description)
        {
            return addSoftwareSystem(Location.Unspecified, name, description);
        }

        /**
         * Creates a software system and adds it to the model
         * (unless one exists with the same name already).
         *
         * @param location      the location of the software system (e.g. internal, external, etc)
         * @param name          the name of the software system
         * @param description   a short description of the software system
         * @return  the SoftwareSystem instance created and added to the model (or null)
         */
        public SoftwareSystem addSoftwareSystem(Location location, String name, String description)
        {
            if (getSoftwareSystemWithName(name) == null)
            {
                SoftwareSystem softwareSystem = new SoftwareSystem();
                softwareSystem.location = (location);
                softwareSystem.name = (name);
                softwareSystem.description = (description);

                _softwareSystems.Add(softwareSystem);

                softwareSystem.id = (idGenerator.generateId(softwareSystem));
                addElementToInternalStructures(softwareSystem);

                return softwareSystem;
            }
            else
            {
                return null;
            }
        }

        /**
         * Creates a person (location is unspecified) and adds it to the model
         * (unless one exists with the same name already).
         *
         * @param name          the name of the person (e.g. "Admin User" or "Bob the Business User")
         * @param description   a short description of the person
         * @return  the Person instance created and added to the model (or null)
         */
        public Person addPerson(String name, String description)
        {
            return addPerson(Location.Unspecified, name, description);
        }

        /**
         * Creates a person and adds it to the model
         * (unless one exists with the same name already).
         *
         * @param location      the location of the person (e.g. internal, external, etc)
         * @param name          the name of the person (e.g. "Admin User" or "Bob the Business User")
         * @param description   a short description of the person
         * @return  the Person instance created and added to the model (or null)
         */
        public Person addPerson(Location location, String name, String description)
        {
            if (getPersonWithName(name) == null)
            {
                Person person = new Person();
                person.setLocation(location);
                person.name = (name);
                person.description = (description);

                _people.Add(person);

                person.id = (idGenerator.generateId(person));
                addElementToInternalStructures(person);

                return person;
            }
            else
            {
                return null;
            }
        }

        public Container addContainer(SoftwareSystem parent, String name, String description, String technology)
        {
            if (parent.getContainerWithName(name) == null)
            {
                Container container = new Container();
                container.name  = (name);
                container.description = (description);
                container.setTechnology(technology);

                container.setParent(parent);
                parent.add(container);

                container.id = idGenerator.generateId(container);
                addElementToInternalStructures(container);

                return container;
            }
            else
            {
                return null;
            }
        }

        public Component addComponentOfType(Container parent, String interfaceType, String implementationType, String description)
        {
            Component component = new Component();
            component.setInterfaceType(interfaceType);
            component.setImplementationType(implementationType);
            component.description = (description);

            component.setParent(parent);
            parent.add(component);

            component.id = idGenerator.generateId(component);
            addElementToInternalStructures(component);

            return component;
        }

        public Component addComponent(Container parent, String name, String description)
        {
            Component component = new Component();
            component.name = (name);
            component.description = (description);

            component.setParent(parent);
            parent.add(component);

            component.id = idGenerator.generateId(component);
            addElementToInternalStructures(component);

            return component;
        }

        public void addRelationship(Relationship relationship)
        {
            if (!relationship.getSource().has(relationship))
            {
                relationship.id = (idGenerator.generateId(relationship));
                relationship.getSource().addRelationship(relationship);
            }
        }

        private void addElementToInternalStructures(Element element)
        {
            elementsById.Add(element.id, element);
            element.setModel(this);
            idGenerator.found(element.id);
        }

        private void addRelationshipToInternalStructures(Relationship relationship)
        {
            relationshipsById.Add(relationship.id, relationship);
            idGenerator.found(relationship.id);
        }

        /**
         * Returns the element in this model with the specified ID
         * (or null if it doesn't exist).
         */
        public Element getElement(String id)
        {
            return elementsById[id];
        }

        /**
         * Returns the relationship in this model with the specified ID
         * (or null if it doesn't exist).
         */
        public Relationship getRelationship(String id)
        {
            return relationshipsById[id];
        }

        public void hydrate()
        {
            // add all of the elements to the model
            foreach (var person in _people)
            {
                addElementToInternalStructures(person);
            }

            foreach (SoftwareSystem softwareSystem in _softwareSystems)
            {
                addElementToInternalStructures(softwareSystem);
                foreach (Container container in softwareSystem.containers)
                {
                    softwareSystem.add(container);
                    addElementToInternalStructures(container);
                    container.setParent(softwareSystem);
                    foreach (Component component in container.getComponents())
                    {
                        container.add(component);
                        addElementToInternalStructures(component);
                        component.setParent(container);
                    }
                }
            }

            // now hydrate the relationships
            foreach (var person in _people)
            {
                hydrateRelationships(person);
            }

            foreach (SoftwareSystem softwareSystem in _softwareSystems)
            {
                hydrateRelationships(softwareSystem);
                foreach (Container container in softwareSystem.containers)
                {
                    hydrateRelationships(container);
                    foreach (Component component in container.getComponents())
                    {
                        hydrateRelationships(component);
                    }
                }
            }
        }

        private void hydrateRelationships(Element element)
        {
            foreach (Relationship relationship in element.relationships)
            {
                relationship.setSource(getElement(relationship.sourceId));
                relationship.setDestination(getElement(relationship.destinationId));
                addRelationshipToInternalStructures(relationship);
            }
        }

        /**
         * Determines whether this model contains the specified element.
         */
        public bool contains(Element element)
        {
            return elementsById.Values.Contains(element);
        }

        /**
         * Gets the SoftwareSystem instance with the specified name
         * (or null if it doesn't exist).
         */
        public SoftwareSystem getSoftwareSystemWithName(String name)
        {
            foreach (SoftwareSystem softwareSystem in softwareSystems)
            {
                if (softwareSystem.name.Equals(name))
                {
                    return softwareSystem;
                }
            }

            return null;
        }

        /**
         * Gets the SoftwareSystem instance with the specified ID
         * (or null if it doesn't exist).
         */
        public SoftwareSystem getSoftwareSystemWithId(String id)
        {
            foreach (SoftwareSystem softwareSystem in softwareSystems)
            {
                if (softwareSystem.id.Equals(id))
                {
                    return softwareSystem;
                }
            }

            return null;
        }

        /**
         * Gets the Person instance with the specified name
         * (or null if it doesn't exist).
         */
        public Person getPersonWithName(String name)
        {
            foreach (Person person in people)
            {
                if (person.name.Equals(name))
                {
                    return person;
                }
            }

            return null;
        }

    }
}