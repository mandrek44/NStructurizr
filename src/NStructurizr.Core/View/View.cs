using System;
using System.Collections.Generic;
using System.Linq;
using NStructurizr.Core.Model;

namespace NStructurizr.Core.View
{
    public abstract class View : IComparable<View> 
    {
        private SoftwareSystem softwareSystem;
        private String _softwareSystemId;
        public String description { get; set; }
        public String key { get; set; }
        public PaperSize paperSize { get; set; }

        public ISet<ElementView> elements { get; set; }

        private ISet<RelationshipView> _relationshipViews = new HashSet<RelationshipView>();

        View() {
            paperSize = PaperSize.A4_Portrait;
            elements = new HashSet<ElementView>();
        }

        internal View(SoftwareSystem softwareSystem, String description) {
            paperSize = PaperSize.A4_Portrait;
            elements = new HashSet<ElementView>();
            this.softwareSystem = softwareSystem;
            this.description = description;
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

        public String softwareSystemId {
            get
            {
                if (this.softwareSystem != null)
                {
                    return this.softwareSystem.id;
                }
                else
                {
                    return this._softwareSystemId;
                }
            }
            set { _softwareSystemId = value; }
        }


        public abstract ViewType type { get; }

       

        /**
     * Adds all software systems in the model to this view.
     */
        public virtual void AddAllSoftwareSystems() {
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
        public void AddAllPeople() {
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
                    elements.Add(new ElementView(element));
                }
            }
        }

        protected void removeElement(Element element) {
            if (element != null) {
                ElementView elementView = new ElementView(element);
                elements.Remove(elementView);
            }
        }

        public abstract void addAllElements();



        public ISet<RelationshipView> relationships
        {
            get
            {
                if (!this._relationshipViews.Any())
                {
                    addRelationships();
                }

                return this._relationshipViews;
            }
            set { this._relationshipViews = relationships; }
        }

        public void addRelationships() {
            ISet<Relationship> relationships = new HashSet<Relationship>();
            ISet<Element> elements = new HashSet<Element>(this.elements
                .Select(e => e.getElement())
                .Where(e => e != null));

            foreach (var b in elements)
            {
                foreach (var r in b.relationships)
                {
                    relationships.Add(r);    
                }
            }

            this._relationshipViews = (new HashSet<RelationshipView>(relationships
                .Where(r => elements.Contains(r.getSource()) && elements.Contains(r.getDestination()))
                .Select(r => new RelationshipView(r))));
        }

        /**
     * Removes all elements that have no relationships
     * to other elements in this view.
     */
        public void removeElementsWithNoRelationships() {
            ISet<RelationshipView> relationships = this.relationships;

            ISet<String> elementIds = new HashSet<string>();
            relationships.ForEach(rv => elementIds.Add(rv.getRelationship().sourceId));
            relationships.ForEach(rv => elementIds.Add(rv.getRelationship().destinationId));

            elements.RemoveIf(ev => !elementIds.Contains(ev.id));
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

                elements.RemoveIf(ev => !elementIdsToShow.Contains(ev.id));
            }
        }

        private void findElementsToShow(Element startingElement, Element element, ISet<String> elementIdsToShow, ISet<String> elementIdsVisited) {
            if (!elementIdsVisited.Contains(element.id) && elements.Contains(new ElementView(element))) {
                elementIdsVisited.Add(element.id);
                elementIdsToShow.Add(element.id);

                // check that we've not gone back to the starting point of the graph
                if (!element.hasEfferentRelationshipWith(startingElement)) {
                    element.relationships.ForEach(r => findElementsToShow(startingElement, r.getDestination(), elementIdsToShow, elementIdsVisited));
                }
            }
        }

        public abstract String name { get; }

        public int CompareTo(View view) {
            return getTitle().CompareTo(view.getTitle());
        }

        // TODO: @JsonIgnore
        public String getTitle() {
            if (description != null && description.Trim().Length > 0) {
                return name + " [" + description+ "]";
            } else {
                return name;
            }
        }

        ElementView findElementView(Element element) {
            foreach (ElementView elementView in elements) {
                if (elementView.getElement().Equals(element)) {
                    return elementView;
                }
            }

            return null;
        }

        RelationshipView findRelationshipView(Relationship relationship) {
            foreach (RelationshipView relationshipView in relationships) {
                if (relationshipView.getRelationship().Equals(relationship)) {
                    return relationshipView;
                }
            }

            return null;
        }

        public void copyLayoutInformationFrom(View source) {
            this.paperSize = (source.paperSize);

            foreach (ElementView sourceElementView in source.elements) {
                ElementView destinationElementView = findElementView(sourceElementView.getElement());
                if (destinationElementView != null) {
                    destinationElementView.copyLayoutInformationFrom(sourceElementView);
                }
            }

            foreach (RelationshipView sourceRelationshipView in source.relationships) {
                RelationshipView destinationRelationshipView = findRelationshipView(sourceRelationshipView.getRelationship());
                if (destinationRelationshipView != null) {
                    destinationRelationshipView.copyLayoutInformationFrom(sourceRelationshipView);
                }
            }
        }

    }
}