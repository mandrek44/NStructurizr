using System;
using Newtonsoft.Json;

namespace NStructurizr.Core.View
{
    public class RelationshipStyle {

        [JsonProperty]
        public String tag { get; private set; }
        [JsonProperty]
        public int? thickness { get; private set; }
        [JsonProperty]
        public String color { get; private set; }
        [JsonProperty]
        public int? fontSize { get; private set; }
        [JsonProperty]
        public int? width { get; private set; }
        [JsonProperty]
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