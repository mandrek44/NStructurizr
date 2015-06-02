using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace NStructurizr.Core.View
{
    public class Styles {
        public Styles()
        {
            elements = new Collection<ElementStyle>();
            relationships = new Collection<RelationshipStyle>();
        }

        [JsonProperty]
        public Collection<ElementStyle> elements { get; private set; }
        [JsonProperty]
        public Collection<RelationshipStyle> relationships { get; private set; }

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