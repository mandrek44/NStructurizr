using System;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ElementView {

        private Element element;
        private String _id;

        public String id
        {
            get
            {
                if (element != null)
                {
                    return element.id;
                }
                else
                {
                    return this._id;
                }
            }
            set { _id = value; }
        }

        public int x { get; set; }
        public int y { get; set; }

        ElementView()
        {
            id = string.Empty;
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