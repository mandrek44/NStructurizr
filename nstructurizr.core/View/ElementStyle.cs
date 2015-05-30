using System;

namespace NStructurizr.Core.View
{
    public class ElementStyle {

        public static readonly int DEFAULT_WIDTH = 450;
        public static readonly int DEFAULT_HEIGHT = 300;

        public String tag { get; private set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        public int? width { get; private set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        public int? height { get; private set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        public String background { get; private set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        public String color { get; private set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        public int? fontSize { get; private set; }

        public ElementStyle() {
        }

        public ElementStyle(String tag) {
            this.tag = tag;
        }

        public ElementStyle(String tag, int? width, int? height, String background, String color, int? fontSize) {
            this.tag = tag;
            this.width = width;
            this.height = height;
            this.background = background;
            this.color = color;
            this.fontSize = fontSize;
        }
    }
}