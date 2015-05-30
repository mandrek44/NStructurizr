using System;
using System.Collections.ObjectModel;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class RelationshipView {

        private Relationship relationship;
        private String id;
        private Collection<Vertex> vertices = new Collection<Vertex>();

        RelationshipView() {
        }

        public RelationshipView(Relationship relationship) {
            this.relationship = relationship;
        }

        public String getId() {
            if (relationship != null) {
                return relationship.id;
            } else {
                return this.id;
            }
        }

        void setId(String id) {
            this.id = id;
        }

        // TODO: @JsonIgnore
        public Relationship getRelationship() {
            return relationship;
        }

        public void setRelationship(Relationship relationship) {
            this.relationship = relationship;
        }

        public Collection<Vertex> getVertices() {
            return vertices;
        }

        public void setVertices(Collection<Vertex> vertices) {
            this.vertices = vertices;
        }

        public void copyLayoutInformationFrom(RelationshipView source) {
            if (source != null) {
                setVertices(source.getVertices());
            }
        }

        public override String ToString() {
            return relationship.ToString();
        }

    }
}