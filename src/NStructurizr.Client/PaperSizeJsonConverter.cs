using System;
using System.Linq;
using Newtonsoft.Json;
using NStructurizr.Core.View;

namespace NStructurizr.Client
{
    public class PaperSizeJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(((PaperSize)value).jsonName);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var sizes = new[]
            {
                PaperSize.A4_Portrait, PaperSize.A4_Landscape, PaperSize.A3_Portrait, PaperSize.A3_Landscape,
                PaperSize.Letter_Landscape, PaperSize.Letter_Portrait, PaperSize.Slide_4_3, PaperSize.Slide_16_9
            };

            string value = (string)reader.Value;
            var paperSize = sizes.FirstOrDefault(size => size.jsonName == value);
            return paperSize;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(PaperSize);
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}