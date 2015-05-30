using System;

namespace NStructurizr.Core.View
{
    public class ElementStyle {

        public static readonly int DEFAULT_WIDTH = 450;
        public static readonly int DEFAULT_HEIGHT = 300;

        private String tag { get; set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int? width { get; set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int? height { get; set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private String background { get; set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private String color { get; set; }

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int? fontSize { get; set; }

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