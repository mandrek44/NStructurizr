using System;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ViewSet
    {

        private Model.Model model;

        private Collection<SystemContextView> _systemContextViews = new Collection<SystemContextView>();

        [JsonProperty]
        private Collection<ContainerView> containerViews = new Collection<ContainerView>();
        [JsonProperty]
        private Collection<ComponentView> componentViews = new Collection<ComponentView>();

        public Collection<SystemContextView> systemContextViews
        {
            get { return new Collection<SystemContextView>(_systemContextViews); }
        }

        private Styles styles = new Styles();

        [JsonProperty]
        public Configuration configuration { get; private set; }

        ViewSet()
        {
        }

        public ViewSet(Model.Model model)
        {
            configuration = new Configuration();
            this.model = model;
        }

        // TODO: @JsonIgnore
        public Model.Model getModel()
        {
            return model;
        }

        public void setModel(Model.Model model)
        {
            this.model = model;
        }

        public SystemContextView createContextView(SoftwareSystem softwareSystem)
        {
            return createContextView(softwareSystem, null);
        }

        public SystemContextView createContextView(SoftwareSystem softwareSystem, String description)
        {
            SystemContextView view = new SystemContextView(softwareSystem, description);
            _systemContextViews.Add(view);

            return view;
        }

        public ContainerView createContainerView(SoftwareSystem softwareSystem)
        {
            return createContainerView(softwareSystem, null);
        }

        public ContainerView createContainerView(SoftwareSystem softwareSystem, String description)
        {
            ContainerView view = new ContainerView(softwareSystem, description);
            containerViews.Add(view);

            return view;
        }

        public ComponentView createComponentView(Container container)
        {
            return createComponentView(container, null);
        }

        public ComponentView createComponentView(Container container, String description)
        {
            ComponentView view = new ComponentView(container, description);
            componentViews.Add(view);

            return view;
        }

        public Collection<ContainerView> getContainerViews()
        {
            return new Collection<ContainerView>(containerViews);
        }

        public Collection<ComponentView> getComponentViews()
        {
            return new Collection<ComponentView>(componentViews);
        }

        public void hydrate()
        {
            _systemContextViews.ForEach(hydrateView);
            containerViews.ForEach(hydrateView);
            componentViews.ForEach(hydrateView);
            foreach (ComponentView view in componentViews)
            {
                hydrateView(view);
                view.setContainer(view.getSoftwareSystem().getContainerWithId(view.containerId));
            }
        }

        private void hydrateView(View view)
        {
            view.setSoftwareSystem(model.getSoftwareSystemWithId(view.softwareSystemId));

            foreach (ElementView elementView in view.elements)
            {
                elementView.setElement(model.getElement(elementView.id));
            }
            foreach (RelationshipView relationshipView in view.relationships)
            {
                relationshipView.setRelationship(model.getRelationship(relationshipView.id));
            }
        }

        //TODO: [Deprecated]
        public Styles getStyles()
        {
            return styles;
        }

        public void copyLayoutInformationFrom(ViewSet source)
        {
            foreach (SystemContextView sourceView in source.systemContextViews)
            {
                SystemContextView destinationView = findSystemContextView(sourceView);
                if (destinationView != null)
                {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }

            foreach (ContainerView sourceView in source.getContainerViews())
            {
                ContainerView destinationView = findContainerView(sourceView);
                if (destinationView != null)
                {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }

            foreach (ComponentView sourceView in source.getComponentViews())
            {
                ComponentView destinationView = findComponentView(sourceView);
                if (destinationView != null)
                {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }
        }

        private SystemContextView findSystemContextView(SystemContextView systemContextView)
        {
            foreach (SystemContextView view in _systemContextViews)
            {
                if (view.getTitle().Equals(systemContextView.getTitle()))
                {
                    return view;
                }
            }

            return null;
        }

        private ContainerView findContainerView(ContainerView containerView)
        {
            foreach (ContainerView view in containerViews)
            {
                if (view.getTitle().Equals(containerView.getTitle()))
                {
                    return view;
                }
            }

            return null;
        }

        private ComponentView findComponentView(ComponentView componentView)
        {
            foreach (ComponentView view in componentViews)
            {
                if (view.getTitle().Equals(componentView.getTitle()))
                {
                    return view;
                }
            }

            return null;
        }

    }
}