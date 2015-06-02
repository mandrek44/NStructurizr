using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NStructurizr.Core.View;

namespace NStructurizr.Core.Client
{
    public class JsonSerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Converters = { new StringEnumConverter(), new PaperSizeJsonConverter() },
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Serialize(Workspace workspace, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(workspace, formatting, _settings);
        }

        public Workspace Deserialize(string response)
        {
            var workspace = JsonConvert.DeserializeObject<Workspace>(response, _settings);
            workspace.hydrate();

            return workspace;
        }
    }
}