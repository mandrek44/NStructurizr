using System.Collections.ObjectModel;

namespace NStructurizr.Core.View
{
    public class Styles {

        private Collection<ElementStyle> elements = new Collection<ElementStyle>();
        private Collection<RelationshipStyle> relationships = new Collection<RelationshipStyle>();

        public Collection<ElementStyle> getElements() {
            return elements;
        }

        public void add(ElementStyle elementStyle) {
            if (elementStyle != null) {
                this.elements.Add(elementStyle);
            }
        }

        public Collection<RelationshipStyle> getRelationships() {
            return relationships;
        }

        public void add(RelationshipStyle relationshipStyle) {
            if (relationshipStyle != null) {
                this.relationships.Add(relationshipStyle);
            }
        }

    }
}