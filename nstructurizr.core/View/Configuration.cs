namespace NStructurizr.Core.View
{
    public class Configuration {
        public Styles styles { get; private set; }

        public Configuration()
        {
            styles = new Styles();
        }
    }
}