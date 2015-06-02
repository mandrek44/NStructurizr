using Newtonsoft.Json;

namespace NStructurizr.Core.View
{
    public class Configuration
    {
        [JsonProperty]
        public Styles styles { get; private set; }

        public Configuration()
        {
            styles = new Styles();
        }
    }
}