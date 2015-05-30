using System;

namespace NStructurizr.Core.Model
{
    public class Relationship : TaggableThing
    {

        protected String id = "";

        private Element source;
        private String sourceId;
        private Element destination;
        private String destinationId;
        private String description;
        private String technology;

        public Relationship()
        {
            addTags(Tags.RELATIONSHIP);
        }

        public Relationship(Element source, Element destination, String description) :
            this(source, destination, description, null)
        {
        }

        public Relationship(Element source, Element destination, String description, String technology)
            : this()
        {
            this.source = source;
            this.destination = destination;
            this.description = description;
            this.technology = technology;
        }

        // TODO: [JsonIgnore]
        public Element getSource()
        {
            return source;
        }

        public String getSourceId()
        {
            if (this.source != null)
            {
                return this.source.getId();
            }
            else
            {
                return this.sourceId;
            }
        }

        public String getId()
        {
            return id;
        }

        public void setId(String id)
        {
            this.id = id;
        }

        void setSourceId(String sourceId)
        {
            this.sourceId = sourceId;
        }

        public void setSource(Element source)
        {
            this.source = source;
        }

        // TODO: [JsonIgnore]
        public Element getDestination()
        {
            return destination;
        }

        public String getDestinationId()
        {
            if (this.destination != null)
            {
                return this.destination.getId();
            }
            else
            {
                return this.destinationId;
            }
        }

        void setDestinationId(String destinationId)
        {
            this.destinationId = destinationId;
        }

        public void setDestination(Element destination)
        {
            this.destination = destination;
        }

        public String getDescription()
        {
            return description != null ? description : "";
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public String getTechnology()
        {
            return technology;
        }

        public void setTechnology(String technology)
        {
            this.technology = technology;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            Relationship that = (Relationship)o;

            if (!getDescription().Equals(that.getDescription())) return false;
            if (!getDestination().Equals(that.getDestination())) return false;
            if (!getSource().Equals(that.getSource())) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = getSourceId().GetHashCode();
            result = 31 * result + getDestinationId().GetHashCode();
            result = 31 * result + getDescription().GetHashCode();
            return result;
        }

        public override String ToString()
        {
            return source.ToString() + " ---[" + description + "]---> " + destination.ToString();
        }

    }
}