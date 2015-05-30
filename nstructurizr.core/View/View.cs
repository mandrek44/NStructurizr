using System;
using System.Collections.Generic;
using System.Linq;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public abstract class View : IComparable<View> {

        private SoftwareSystem softwareSystem;
        private String softwareSystemId;
        private String description = "";
        private String key;
        private PaperSize paperSize = PaperSize.A4_Portrait;

        private ISet<ElementView> elementViews = new HashSet<ElementView>();

        private ISet<RelationshipView> relationshipViews = new HashSet<RelationshipView>();

        View() {
        }

        internal View(SoftwareSystem softwareSystem, String description) {
            this.softwareSystem = softwareSystem;
            setDescription(description);
        }

        // TODO: @JsonIgnore
        public Model.Model getModel() {
            return softwareSystem.getModel();
        }

        // TODO: @JsonIgnore
        public SoftwareSystem getSoftwareSystem() {
            return softwareSystem;
        }

        public void setSoftwareSystem(SoftwareSystem softwareSystem) {
            this.softwareSystem = softwareSystem;
        }

        public String getSoftwareSystemId() {
            if (this.softwareSystem != null) {
                return this.softwareSystem.id;
            } else {
                return this.softwareSystemId;
            }
        }

        void setSoftwareSystemId(String softwareSystemId) {
            this.softwareSystemId = softwareSystemId;
        }

        public abstract ViewType getType();

        public String getDescription() {
            return description;
        }

        public void setDescription(String description) {
            if (description == null) {
                this.description = "";
            } else {
                this.description = description;
            }
        }

        public String getKey() {
            return key;
        }

        public void setKey(String key) {
            this.key = key;
        }

        public PaperSize getPaperSize() {
            return paperSize;
        }

        public void setPaperSize(PaperSize paperSize) {
            this.paperSize = paperSize;
        }

        /**
     * Adds all software systems in the model to this view.
     */
        public virtual void addAllSoftwareSystems() {
            foreach (var system in getModel().softwareSystems)
            {
                addElement(system);
            }
        }

        /**
     * Adds the given software system to this view.
     *
     * @param softwareSystem        the SoftwareSystem to add
     */
        public void addSoftwareSystem(SoftwareSystem softwareSystem) {
            addElement(softwareSystem);
        }

        /**
     * Adds all software systems in the model to this view.
     */
        public void addAllPeople() {
            foreach (var person in getModel().people)
            {
                addElement(person);
            }
        }

        /**
     * Adds the given person to this view.
     *
     * @param person        the Person to add
     */
        public void addPerson(Person person) {
            addElement(person);
        }

        protected void addElement(Element element) {
            if (element != null) {
                if (softwareSystem.getModel().contains(element)) {
                    elementViews.Add(new ElementView(element));
                }
            }
        }

        protected void removeElement(Element element) {
            if (element != null) {
                ElementView elementView = new ElementView(element);
                elementViews.Remove(elementView);
            }
        }

        /**
     * Gets the set of elements in this view.
     *
     * @return  a Set of ElementView objects
     */
        public ISet<ElementView> getElements() {
            return elementViews;
        }

        void setElements(ISet<ElementView> elementViews) {
            this.elementViews = elementViews;
        }

        public abstract void addAllElements();

        public ISet<RelationshipView> getRelationships() {
            if (!this.relationshipViews.Any()) {
                addRelationships();
            }

            return this.relationshipViews;
        }

        public void setRelationships(ISet<RelationshipView> relationships) {
            this.relationshipViews = relationships;
        }

        public void addRelationships() {
            ISet<Relationship> relationships = new HashSet<Relationship>();
            ISet<Element> elements = new HashSet<Element>(getElements()
                .Select(e => e.getElement()));

            foreach (var b in elements)
            {
                foreach (var r in b.relationships)
                {
                    relationships.Add(r);    
                }
            }

            setRelationships(new HashSet<RelationshipView>(relationships
                .Where(r => elements.Contains(r.getSource()) && elements.Contains(r.getDestination()))
                .Select(r => new RelationshipView(r))));
        }

        /**
     * Removes all elements that have no relationships
     * to other elements in this view.
     */
        public void removeElementsWithNoRelationships() {
            ISet<RelationshipView> relationships = getRelationships();

            ISet<String> elementIds = new HashSet<string>();
            relationships.ForEach(rv => elementIds.Add(rv.getRelationship().sourceId));
            relationships.ForEach(rv => elementIds.Add(rv.getRelationship().destinationId));

            elementViews.RemoveIf(ev => !elementIds.Contains(ev.getId()));
        }

        /**
     * Removes all elements that cannot be reached by traversing the graph of relationships
     * starting with the specified element.
     *
     * @param element       the starting element
     */
        public void removeElementsThatCantBeReachedFrom(Element element) {
            if (element != null) {
                ISet<String> elementIdsToShow = new HashSet<string>();
                ISet<String> elementIdsVisited = new HashSet<string>();
                findElementsToShow(element, element, elementIdsToShow, elementIdsVisited);

                elementViews.RemoveIf(ev => !elementIdsToShow.Contains(ev.getId()));
            }
        }

        private void findElementsToShow(Element startingElement, Element element, ISet<String> elementIdsToShow, ISet<String> elementIdsVisited) {
            if (!elementIdsVisited.Contains(element.id) && elementViews.Contains(new ElementView(element))) {
                elementIdsVisited.Add(element.id);
                elementIdsToShow.Add(element.id);

                // check that we've not gone back to the starting point of the graph
                if (!element.hasEfferentRelationshipWith(startingElement)) {
                    element.relationships.ForEach(r => findElementsToShow(startingElement, r.getDestination(), elementIdsToShow, elementIdsVisited));
                }
            }
        }

        public abstract String getName();

        public int CompareTo(View view) {
            return getTitle().CompareTo(view.getTitle());
        }

        // TODO: @JsonIgnore
        public String getTitle() {
            if (getDescription() != null && getDescription().Trim().Length > 0) {
                return getName() + " [" + getDescription() + "]";
            } else {
                return getName();
            }
        }

        ElementView findElementView(Element element) {
            foreach (ElementView elementView in getElements()) {
                if (elementView.getElement().Equals(element)) {
                    return elementView;
                }
            }

            return null;
        }

        RelationshipView findRelationshipView(Relationship relationship) {
            foreach (RelationshipView relationshipView in getRelationships()) {
                if (relationshipView.getRelationship().Equals(relationship)) {
                    return relationshipView;
                }
            }

            return null;
        }

        public void copyLayoutInformationFrom(View source) {
            this.setPaperSize(source.getPaperSize());

            foreach (ElementView sourceElementView in source.getElements()) {
                ElementView destinationElementView = findElementView(sourceElementView.getElement());
                if (destinationElementView != null) {
                    destinationElementView.copyLayoutInformationFrom(sourceElementView);
                }
            }

            foreach (RelationshipView sourceRelationshipView in source.getRelationships()) {
                RelationshipView destinationRelationshipView = findRelationshipView(sourceRelationshipView.getRelationship());
                if (destinationRelationshipView != null) {
                    destinationRelationshipView.copyLayoutInformationFrom(sourceRelationshipView);
                }
            }
        }

    }
}