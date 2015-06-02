using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NStructurizr.Core.View;

namespace NStructurizr.Core.Client
{
    public class JsonSerializer
    {
        public string Serialize(Workspace workspace, Formatting formatting = Formatting.None)
        {
            var settings = new JsonSerializerSettings()
            {
                Converters = { new StringEnumConverter(), new PaperSizeJsonConverter() },
                NullValueHandling = NullValueHandling.Ignore
            };

            return JsonConvert.SerializeObject(workspace, formatting, settings);
        }
    }
}