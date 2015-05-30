using System;
using System.Collections.ObjectModel;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class RelationshipView
    {

        private Relationship relationship;
        public String id { get; set; }
        public Collection<Vertex> vertices { get; set; }

        RelationshipView()
        {
            vertices = new Collection<Vertex>();
        }

        public RelationshipView(Relationship relationship)
        {
            vertices = new Collection<Vertex>();
            this.relationship = relationship;
        }

        // TODO: @JsonIgnore
        public Relationship getRelationship()
        {
            return relationship;
        }

        public void setRelationship(Relationship relationship)
        {
            this.relationship = relationship;
        }

        public void copyLayoutInformationFrom(RelationshipView source)
        {
            if (source != null)
            {
                vertices = source.vertices;
            }
        }

        public override String ToString()
        {
            return relationship.ToString();
        }

    }
}