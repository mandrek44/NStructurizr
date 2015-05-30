using System;
using System.Collections.ObjectModel;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public class ViewSet {

        private Model.Model model;

        private Collection<SystemContextView> systemContextViews = new Collection<SystemContextView>();
        private Collection<ContainerView> containerViews = new Collection<ContainerView>();
        private Collection<ComponentView> componentViews = new Collection<ComponentView>();

        private Styles styles = new Styles();
        private Configuration configuration = new Configuration();

        ViewSet() {
        }

        public ViewSet(Model.Model model) {
            this.model = model;
        }

        // TODO: @JsonIgnore
        public Model.Model getModel() {
            return model;
        }

        public void setModel(Model.Model model) {
            this.model = model;
        }

        public SystemContextView createContextView(SoftwareSystem softwareSystem) {
            return createContextView(softwareSystem, null);
        }

        public SystemContextView createContextView(SoftwareSystem softwareSystem, String description) {
            SystemContextView view = new SystemContextView(softwareSystem, description);
            systemContextViews.Add(view);

            return view;
        }

        public ContainerView createContainerView(SoftwareSystem softwareSystem) {
            return createContainerView(softwareSystem, null);
        }

        public ContainerView createContainerView(SoftwareSystem softwareSystem, String description) {
            ContainerView view = new ContainerView(softwareSystem, description);
            containerViews.Add(view);

            return view;
        }

        public ComponentView createComponentView(Container container) {
            return createComponentView(container, null);
        }

        public ComponentView createComponentView(Container container, String description) {
            ComponentView view = new ComponentView(container, description);
            componentViews.Add(view);

            return view;
        }

        public Collection<SystemContextView> getSystemContextViews() {
            return new Collection<SystemContextView>(systemContextViews);
        }

        public Collection<ContainerView> getContainerViews() {
            return new Collection<ContainerView>(containerViews);
        }

        public Collection<ComponentView> getComponentViews() {
            return new Collection<ComponentView>(componentViews);
        }

        public void hydrate() {
            systemContextViews.ForEach(hydrateView);
            containerViews.ForEach(hydrateView);
            componentViews.ForEach(hydrateView);
            foreach (ComponentView view in componentViews) {
                hydrateView(view);
                view.setContainer(view.getSoftwareSystem().getContainerWithId(view.getContainerId()));
            }
        }

        private void hydrateView(View view) {
            view.setSoftwareSystem(model.getSoftwareSystemWithId(view.getSoftwareSystemId()));

            foreach (ElementView elementView in view.getElements()) {
                elementView.setElement(model.getElement(elementView.getId()));
            }
            foreach (RelationshipView relationshipView in view.getRelationships()) {
                relationshipView.setRelationship(model.getRelationship(relationshipView.getId()));
            }
        }

        //TODO: [Deprecated]
        public Styles getStyles() {
            return styles;
        }

        public Configuration getConfiguration() {
            return configuration;
        }

        public void copyLayoutInformationFrom(ViewSet source) {
            foreach (SystemContextView sourceView in source.getSystemContextViews()) {
                SystemContextView destinationView = findSystemContextView(sourceView);
                if (destinationView != null) {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }

            foreach (ContainerView sourceView in source.getContainerViews()) {
                ContainerView destinationView = findContainerView(sourceView);
                if (destinationView != null) {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }

            foreach (ComponentView sourceView in source.getComponentViews()) {
                ComponentView destinationView = findComponentView(sourceView);
                if (destinationView != null) {
                    destinationView.copyLayoutInformationFrom(sourceView);
                }
            }
        }

        private SystemContextView findSystemContextView(SystemContextView systemContextView) {
            foreach (SystemContextView view in systemContextViews) {
                if (view.getTitle().Equals(systemContextView.getTitle())) {
                    return view;
                }
            }

            return null;
        }

        private ContainerView findContainerView(ContainerView containerView) {
            foreach (ContainerView view in containerViews) {
                if (view.getTitle().Equals(containerView.getTitle())) {
                    return view;
                }
            }

            return null;
        }

        private ComponentView findComponentView(ComponentView componentView) {
            foreach (ComponentView view in componentViews) {
                if (view.getTitle().Equals(componentView.getTitle())) {
                    return view;
                }
            }

            return null;
        }

    }
}