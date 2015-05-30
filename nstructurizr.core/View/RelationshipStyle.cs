using System;

namespace NStructurizr.Core.View
{
    public class RelationshipStyle {

        private String tag { get; set; }

        private int? thickness { get; set; }

        private String color { get; set; }

        private int? fontSize { get; set; }

        private int? width { get; set; }

        private Boolean dashed { get; set; }

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