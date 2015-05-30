using System;

namespace NStructurizr.Core.View
{
    public class ElementStyle {

        public static readonly int DEFAULT_WIDTH = 450;
        public static readonly int DEFAULT_HEIGHT = 300;

        private String tag;

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int width;

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int height;

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private String background;

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private String color;

        // TODO: @JsonInclude(value = JsonInclude.Include.NON_NULL)
        private int fontSize;

        public ElementStyle() {
        }

        public ElementStyle(String tag) {
            this.tag = tag;
        }

        public ElementStyle(String tag, int width, int height, String background, String color, int fontSize) {
            this.tag = tag;
            this.width = width;
            this.height = height;
            this.background = background;
            this.color = color;
            this.fontSize = fontSize;
        }

        public String getTag() {
            return tag;
        }

        public void setTag(String tag) {
            this.tag = tag;
        }

        public int getWidth() {
            return width;
        }

        public void setWidth(int width) {
            this.width = width;
        }

        public int getHeight() {
            return height;
        }

        public void setHeight(int height) {
            this.height = height;
        }

        public String getBackground() {
            return background;
        }

        public void setBackground(String background) {
            this.background = background;
        }

        public String getColor() {
            return color;
        }

        public void setColor(String color) {
            this.color = color;
        }

        public int getFontSize() {
            return fontSize;
        }

        public void setFontSize(int fontSize) {
            this.fontSize = fontSize;
        }

    }
}