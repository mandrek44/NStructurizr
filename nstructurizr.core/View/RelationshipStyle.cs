using System;

namespace NStructurizr.Core.View
{
    public class RelationshipStyle {
        public String tag { get; private set; }

        public int? thickness { get; private set; }

        public String color { get; private set; }

        public int? fontSize { get; private set; }

        public int? width { get; private set; }

        public Boolean dashed { get; private set; }

        public RelationshipStyle() {
        }

        public RelationshipStyle(String tag) {
            this.tag = tag;
        }

        public RelationshipStyle(String tag, int? thickness, String color, Boolean dashed, int? fontSize, int? width) {
            this.tag = tag;
            this.thickness = thickness;
            this.color = color;
            this.dashed = dashed;
            this.fontSize = fontSize;
            this.width = width;
        }

    }
}