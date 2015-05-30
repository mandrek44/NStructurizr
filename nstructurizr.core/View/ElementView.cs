using System;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ElementView {

        private Element element;
        private String id;
        private int x;
        private int y;

        ElementView() {
        }

        public ElementView(Element element) {
            this.element = element;
        }

        //  TODO: @JsonIgnore
        public Element getElement() {
            return element;
        }

        public void setElement(Element element) {
            this.element = element;
        }

        public String getId() {
            if (element != null) {
                return element.getId();
            } else {
                return this.id;
            }
        }

        void setId(String id) {
            this.id = id;
        }

        public int getX() {
            return x;
        }

        public void setX(int x) {
            this.x = x;
        }

        public int getY() {
            return y;
        }

        public void setY(int y) {
            this.y = y;
        }

        public override bool Equals(Object o) {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            ElementView that = (ElementView) o;

            if (!getId().Equals(that.getId())) return false;

            return true;
        }

        public override int GetHashCode() {
            return getId().GetHashCode();
        }

        public override String ToString() {
            return getElement().ToString();
        }

        public void copyLayoutInformationFrom(ElementView source) {
            if (source != null) {
                setX(source.getX());
                setY(source.getY());
            }
        }

    }
}