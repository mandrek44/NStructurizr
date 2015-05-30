using System;

namespace NStructurizr.Core.View
{
    public class PaperSize
    {

        public static readonly PaperSize A4_Portrait = new PaperSize("A4", Orientation.Portrait, 2480, 3510);
        public static readonly PaperSize A4_Landscape = new PaperSize("A4", Orientation.Landscape, 3510, 2480);

        public static readonly PaperSize A3_Portrait = new PaperSize("A3", Orientation.Portrait, 3510, 4950);
        public static readonly PaperSize A3_Landscape = new PaperSize("A3", Orientation.Landscape, 4950, 3510);

        public static readonly PaperSize Letter_Portrait = new PaperSize("Letter", Orientation.Portrait, 2550, 3300);
        public static readonly PaperSize Letter_Landscape = new PaperSize("Letter", Orientation.Landscape, 3300, 2550);

        public static readonly PaperSize Slide_4_3 = new PaperSize("Slide 4:3", Orientation.Landscape, 3306, 2480);
        public static readonly PaperSize Slide_16_9 = new PaperSize("Slide 16:9", Orientation.Landscape, 3510, 1974);

        public String name { get; private set; }
        public Orientation orientation { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        private PaperSize(String name, Orientation orientation, int width, int height) {
            this.name = name;
            this.orientation = orientation;
            this.width = width;
            this.height = height;
        }

        public enum Orientation {
            Portrait,
            Landscape
        }

    }
}