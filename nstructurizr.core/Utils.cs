using System;
using Newtonsoft.Json;

namespace NStructurizr.Core
{
    public class Utils
    {
         
    }

    public class IntEnumConverter : Newtonsoft.Json.Converters.StringEnumConverter
    {
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value != null)
            {
                Type type = value.GetType();

                if (type.IsEnum)
                {
                    Type underlyingType = Enum.GetUnderlyingType(type);

                    value = Convert.ChangeType(value, underlyingType);

                    writer.WriteValue(value);
                }
                return;
            }

            base.WriteJson(writer, value, serializer);
        }
    }
}