using System;
using Newtonsoft.Json;
using NStructurizr.Client;
using NStructurizr.Core;
using NStructurizr.Core.Model;
using NStructurizr.Core.View;
using JsonSerializer = NStructurizr.Client.JsonSerializer;

namespace NStructurizr.Examples
{
    public class FinancialRiskSystem {

        private static readonly String TAG_ASYNCHRONOUS = "Asynchronous";
        private static readonly String TAG_ALERT = "Alert";

        public static void Run() {
            Workspace workspace = new Workspace("Financial Risk System", "This is a simple (incomplete) example C4 model based upon the financial risk system architecture kata, which can be found at http://bit.ly/sa4d-risksystem");
            workspace.id = 1491;
            Model model = workspace.model;

            // create the basic model
            SoftwareSystem financialRiskSystem = model.AddSoftwareSystem(Location.Internal, "Financial Risk System", "Calculates the bank's exposure to risk for product X");

            Person businessUser = model.AddPerson(Location.Internal, "Business User", "A regular business user");
            businessUser.Uses(financialRiskSystem, "Views reports");

            Person configurationUser = model.AddPerson(Location.Internal, "Configuration User", "A regular business user who can also configure the parameters used in the risk calculations");
            configurationUser.Uses(financialRiskSystem, "Configures parameters");

            SoftwareSystem tradeDataSystem = model.AddSoftwareSystem(Location.Internal, "Trade Data System", "The system of record for trades of type X");
            financialRiskSystem.Uses(tradeDataSystem, "Gets trade data from");

            SoftwareSystem referenceDataSystem = model.AddSoftwareSystem(Location.Internal, "Reference Data System", "Manages reference data for all counterparties the bank interacts with");
            financialRiskSystem.Uses(referenceDataSystem, "Gets counterparty data from");

            SoftwareSystem emailSystem = model.AddSoftwareSystem(Location.Internal, "E-mail system", "Microsoft Exchange");
            financialRiskSystem.Uses(emailSystem, "Sends a notification that a report is ready via e-mail to");
            emailSystem.delivers(businessUser, "Sends a notification that a report is ready via e-mail to").addTags(TAG_ASYNCHRONOUS);

            SoftwareSystem centralMonitoringService = model.AddSoftwareSystem(Location.Internal, "Central Monitoring Service", "The bank-wide monitoring and alerting dashboard");
            financialRiskSystem.Uses(centralMonitoringService, "Sends critical failure alerts to").addTags(TAG_ALERT);

            SoftwareSystem activeDirectory = model.AddSoftwareSystem(Location.Internal, "Active Directory", "Manages users and security roles across the bank");
            financialRiskSystem.Uses(activeDirectory, "Uses for authentication and authorisation");

            // create some views
            ViewSet viewSet = workspace.views;
            SystemContextView contextView = viewSet.createContextView(financialRiskSystem);
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // tag and style some elements
            financialRiskSystem.addTags("Risk System");
            viewSet.configuration.styles.add(new ElementStyle("Risk System", null, null, "#550000", "#ffffff", null));
            viewSet.configuration.styles.add(new ElementStyle(Tags.SOFTWARE_SYSTEM, null, null, "#801515", "#ffffff", null));
            viewSet.configuration.styles.add(new ElementStyle(Tags.PERSON, null, null, "#d46a6a", "#ffffff", null));
            viewSet.configuration.styles.add(new RelationshipStyle(Tags.RELATIONSHIP, null, null, false, null, null));
            viewSet.configuration.styles.add(new RelationshipStyle(TAG_ASYNCHRONOUS, null, null, true, null, null));
            viewSet.configuration.styles.add(new RelationshipStyle(TAG_ALERT, null, "#ff0000", false, null, null));

            // output the model as JSON
            Console.WriteLine(new JsonSerializer().Serialize(workspace, Formatting.Indented));
            
            // and upload the model to structurizr.com
            StructurizrClient structurizrClient = new StructurizrClient("https://api.structurizr.com", "api", "key");

            structurizrClient.MergeWorkspace(1491, workspace);
            
        }

    }
}