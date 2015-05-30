using System;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ElementView {

        private Element element;
        public String id { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        ElementView()
        {
            id = string.Empty;
        }

        public ElementView(Element element) {
            id = string.Empty;
            this.element = element;
        }

        //  TODO: @JsonIgnore
        public Element getElement() {
            return element;
        }

        public void setElement(Element element) {
            this.element = element;
        }

        public override bool Equals(Object o) {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            ElementView that = (ElementView) o;

            if (!id.Equals(that.id)) return false;

            return true;
        }

        public override int GetHashCode() {
            return id.GetHashCode();
        }

        public override String ToString() {
            return getElement().ToString();
        }

        public void copyLayoutInformationFrom(ElementView source) {
            if (source != null) {
                x = (source.x);
                y = (source.y);
            }
        }

    }
}