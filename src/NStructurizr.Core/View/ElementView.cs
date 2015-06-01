using System;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ElementView
    {
        private Element _element;
        private String _id;

        public String id
        {
            get
            {
                if (_element != null)
                {
                    return _element.id;
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
        
        public ElementView(Element element)
        {
            this._element = element;
        }

        //  TODO: @JsonIgnore
        public Element getElement()
        {
            return _element;
        }

        public void setElement(Element element)
        {
            this._element = element;
        }

        public override bool Equals(Object o)
        {
            if (this == o) return true;
            if (o == null || GetType() != o.GetType()) return false;

            ElementView that = (ElementView)o;

            if (!id.Equals(that.id)) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }

        public override String ToString()
        {
            return getElement().ToString();
        }

        public void copyLayoutInformationFrom(ElementView source)
        {
            if (source != null)
            {
                x = (source.x);
                y = (source.y);
            }
        }

    }
}