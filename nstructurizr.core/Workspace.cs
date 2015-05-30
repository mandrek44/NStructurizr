using System;
using NStructurizr.Core.View;

namespace NStructurizr.Core
{

    
/**
 * These represent paper sizes in pixels at 300dpi.
 */


    public class Workspace
    {
        private long id;
        private String name;
        private String description;
        private Model.Model model = new Model.Model();
        private ViewSet viewSet;


        public Workspace(String name, String description)
        {
            viewSet = new ViewSet(model);
            this.name = name;
            this.description = description;
        }

        public long getId()
        {
            return this.id;
        }

        public void setId(long id)
        {
            this.id = id;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public String getDescription()
        {
            return description;
        }

        public void setDescription(String description)
        {
            this.description = description;
        }

        public Model.Model getModel()
        {
            return model;
        }

        public void setModel(Model.Model model)
        {
            this.model = model;
        }

        public ViewSet getViews()
        {
            return viewSet;
        }

        public void setViews(ViewSet viewSet)
        {
            this.viewSet = viewSet;
        }

        public void hydrate()
        {
            this.viewSet.setModel(model);
            this.model.hydrate();
            this.viewSet.hydrate();
        }

    }
}