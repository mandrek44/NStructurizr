using System;
using NStructurizr.Core.View;

namespace NStructurizr.Core
{
    public class Workspace
    {
        public long id { get; set; }
        public String name { get; set; }
        public String description { get; set; }
        public Model.Model model { get; set; }
        public ViewSet views { get; set; }

        public Workspace(String name, String description)
        {
            model = new Model.Model();
            views = new ViewSet(model);
            this.name = name;
            this.description = description;
        }

        public void hydrate()
        {
            this.views.setModel(model);
            this.model.hydrate();
            this.views.hydrate();
        }
    }
}