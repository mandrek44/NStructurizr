using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NStructurizr.Core.Model
{
    public abstract class Element : TaggableThing
    {

        public static readonly String CANONICAL_NAME_SEPARATOR = "/";

        private Model model;
        public String id { get; set; }

        public virtual String name { get; set; }
        public String description { get; set; }

        [JsonProperty("relationships")]
        protected ISet<Relationship> _relationships = new HashSet<Relationship>();

        [JsonIgnore]
        public ISet<Relationship> relationships
        {
            get
            {
                return new HashSet<Relationship>(_relationships);
            }
        }

        protected Element()
        {
            addTags(Tags.ELEMENT);
        }

        // TODO: @JsonIgnore
        public Model getModel()
        {
            return this.model;
        }

        public void setModel(Model model)
        {
            this.model = model;
        }

        public bool has(Relationship relationship)
        {
            return _relationships.Contains(relationship);
        }

        public void addRelationship(Relationship relationship)
        {
            _relationships.Add(relationship);
        }

        public override string ToString()
        {
            return "{" + id + " | " + name + " | " + description + "}";
        }

        public abstract ElementType type { get; }

        // TODO: @JsonIgnore
        public abstract String getCanonicalName();

        protected String formatForCanonicalName(String name)
        {
            return name.Replace(CANONICAL_NAME_SEPARATOR, "");
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and another.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         */
        public Relationship uses(SoftwareSystem destination, String description)
        {
            Relationship relationship = new Relationship(this, destination, description);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and another.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         * @param technology    the technology details (e.g. JSON/HTTPS)
         */
        public Relationship uses(SoftwareSystem destination, String description, String technology)
        {
            Relationship relationship = new Relationship(this, destination, description, technology);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and a container.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         */
        public Relationship uses(Container destination, String description)
        {
            Relationship relationship = new Relationship(this, destination, description);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and a container.
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         * @param technology    the technology details (e.g. JSON/HTTPS)
         */
        public Relationship uses(Container destination, String description, String technology)
        {
            Relationship relationship = new Relationship(this, destination, description, technology);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and a component (within a container).
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         */
        public Relationship uses(Component destination, String description)
        {
            Relationship relationship = new Relationship(this, destination, description);
            getModel().addRelationship(relationship);

            return relationship;
        }

        /**
         * Adds a unidirectional "uses" style relationship between this element
         * and a component (within a container).
         *
         * @param destination   the target of the relationship
         * @param description   a description of the relationship (e.g. "uses", "gets data from", "sends data to")
         * @param technology    the technology details (e.g. JSON/HTTPS)
         */
        public Relationship uses(Component destination, String description, String technology)
        {
            Relationship relationship = new Relationship(this, destination, description, technology);
            getModel().addRelationship(relationship);

            return relationship;
        }

        public bool hasEfferentRelationshipWith(Element element)
        {
            if (element == null)
            {
                return false;
            }

            foreach (Relationship relationship in _relationships)
            {
                if (relationship.getDestination().Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool Equals(Object o)
        {
            if (this == o)
            {
                return true;
            }

            if (o == null || !(o is Element))
            {
                return false;
            }

            Element element = (Element)o;
            return getCanonicalName().Equals(element.getCanonicalName());
        }

    }
}