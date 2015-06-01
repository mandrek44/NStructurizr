using System;
using Newtonsoft.Json;

namespace NStructurizr.Core.View
{
    public class PaperSizeJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(((PaperSize)value).jsonName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof (PaperSize);
        }
    }

    public class PaperSize
    {
        public string jsonName { get; private set; }

        public static readonly PaperSize A4_Portrait = new PaperSize("A4", "A4_Portrait", Orientation.Portrait, 2480, 3510);
        public static readonly PaperSize A4_Landscape = new PaperSize("A4", "A4_Landscape", Orientation.Landscape, 3510, 2480);

        public static readonly PaperSize A3_Portrait = new PaperSize("A3", "A3_Portrait", Orientation.Portrait, 3510, 4950);
        public static readonly PaperSize A3_Landscape = new PaperSize("A3", "A3_Landscape",Orientation.Landscape, 4950, 3510);

        public static readonly PaperSize Letter_Portrait = new PaperSize("Letter", "Letter_Portrait", Orientation.Portrait, 2550, 3300);
        public static readonly PaperSize Letter_Landscape = new PaperSize("Letter", "Letter_Landscape", Orientation.Landscape, 3300, 2550);

        public static readonly PaperSize Slide_4_3 = new PaperSize("Slide 4:3", "Slide_4_3", Orientation.Landscape, 3306, 2480);
        public static readonly PaperSize Slide_16_9 = new PaperSize("Slide 16:9", "Slide_16_9", Orientation.Landscape, 3510, 1974);

        public String name { get; private set; }
        public Orientation orientation { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        private PaperSize(string name, string jsonName, Orientation orientation, int width, int height) {
            this.jsonName = jsonName;
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