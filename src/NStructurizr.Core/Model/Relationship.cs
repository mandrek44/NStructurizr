using System;

namespace NStructurizr.Core.Model
{
    public class Relationship : TaggableThing
    {
        public String id { get; set; }

        private Element source;
        private Element destination;
        private string _destinationId;
        private string _sourceId;


        public String sourceId
        {
            get
            {
                if (this.source != null)
                {
                    return this.source.id;
                }
                else
                {
                    return this._sourceId;
                }
            }
            set { _sourceId = value; }
        }

        public String destinationId
        {
            get
            {
                if (this.destination != null)
                {
                    return this.destination.id;
                }
                else
                {
                    return this._destinationId;
                }
            }
            set { _destinationId = value; }
        }

        public String description { get; set; }
        public String technology { get; set; }

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


        public void setSource(Element source)
        {
            this.source = source;
        }

        // TODO: [JsonIgnore]
        public Element getDestination()
        {
            return destination;
        }

        public void setDestination(Element destination)
        {
            this.destination = destination;
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            Relationship that = (Relationship)o;

            if (!description.Equals(that.description)) return false;
            if (!getDestination().Equals(that.getDestination())) return false;
            if (!getSource().Equals(that.getSource())) return false;

            return true;
        }

        public override int GetHashCode()
        {
            int result = sourceId.GetHashCode();
            result = 31 * result + destinationId.GetHashCode();
            result = 31 * result + description.GetHashCode();
            return result;
        }

        public override String ToString()
        {
            return source.ToString() + " ---[" + description + "]---> " + destination.ToString();
        }

    }
}