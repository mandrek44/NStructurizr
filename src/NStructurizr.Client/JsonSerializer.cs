using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NStructurizr.Core;

namespace NStructurizr.Client
{
    public class JsonSerializer
    {
        private readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            Converters = { new StringEnumConverter(), new PaperSizeJsonConverter() },
            NullValueHandling = NullValueHandling.Ignore
        };

        public string Serialize(Workspace workspace, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(workspace, formatting, this.settings);
        }

        public Workspace Deserialize(string response)
        {
            var workspace = JsonConvert.DeserializeObject<Workspace>(response, this.settings);
            workspace.hydrate();

            return workspace;
        }
    }
}