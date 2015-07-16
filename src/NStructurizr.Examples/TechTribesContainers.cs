using System;
using Newtonsoft.Json;
using NStructurizr.Client;
using NStructurizr.Core;
using NStructurizr.Core.Model;
using NStructurizr.Core.View;
using JsonSerializer = NStructurizr.Client.JsonSerializer;

namespace NStructurizr.Examples
{
    public class TechTribesContainers
    {
        public static void Run()
        {
            // create a model and the software system we want to describe
            Workspace workspace = new Workspace("techtribes.je",
                "This is a model of the system context for the techtribes.je system, the code for which can be found at https://github.com/techtribesje/techtribesje");
            workspace.id = 1561;
            Model model = workspace.model;

            SoftwareSystem techTribes = model.AddSoftwareSystem(Location.Internal, "techtribes.je",
                "techtribes.je is the only way to keep up to date with the IT, tech and digital sector in Jersey and Guernsey, Channel Islands");

            // create the various types of people (roles) that use the software system
            Person anonymousUser = model.AddPerson(Location.External, "Anonymous User", "Anybody on the web.");
            anonymousUser.Uses(techTribes,
                "View people, tribes (businesses, communities and interest groups), content, events, jobs, etc from the local tech, digital and IT sector.");

            Person authenticatedUser = model.AddPerson(Location.External, "Aggregated User",
                "A user or business with content that is aggregated into the website.");
            authenticatedUser.Uses(techTribes, "Manage user profile and tribe membership.");

            Person adminUser = model.AddPerson(Location.External, "Administration User", "A system administration user.");
            adminUser.Uses(techTribes, "Add people, add tribes and manage tribe membership.");

            // create the various software systems that techtribes.je has a dependency on
            SoftwareSystem twitter = model.AddSoftwareSystem(Location.External, "Twitter", "twitter.com");
            techTribes.Uses(twitter, "Gets profile information and tweets from.");

            SoftwareSystem gitHub = model.AddSoftwareSystem(Location.External, "GitHub", "github.com");
            techTribes.Uses(gitHub, "Gets information about public code repositories from.");

            SoftwareSystem blogs = model.AddSoftwareSystem(Location.External, "Blogs", "RSS and Atom feeds");
            techTribes.Uses(blogs, "Gets content using RSS and Atom feeds from.");

            // create the containers that techtribes.je is made up from
            Container webApplication = techTribes.addContainer("Web Application",
                "Allows users to view people, tribes, content, events, jobs, etc from the local tech, digital and IT sector.",
                "Apache Tomcat 7.x");
            Container contentUpdater = techTribes.addContainer("Content Updater",
                "Updates profiles, tweets, GitHub repos and content on a scheduled basis.",
                "Standalone Java 7 application");
            Container relationalDatabase = techTribes.addContainer("Relational Database",
                "Stores people, tribes, tribe membership, talks, events, jobs, badges, GitHub repos, etc.",
                "MySQL 5.5.x");
            Container noSqlStore = techTribes.addContainer("NoSQL Data Store",
                "Stores content from RSS/Atom feeds (blog posts) and tweets.", "MongoDB 2.2.x");
            Container fileSystem = techTribes.addContainer("File System", "Stores search indexes.", null);

            Component test = webApplication.addComponent("TestComponent", "test component");
            Component test2 = webApplication.addComponent("TestComponent2", "test component 2");

            test.Uses(test2, "calls");
            

            anonymousUser.Uses(webApplication,
                "View people, tribes (businesses, communities and interest groups), content, events, jobs, etc from the local tech, digital and IT sector.");
            authenticatedUser.Uses(webApplication, "Manage user profile and tribe membership.");
            adminUser.Uses(webApplication, "Add people, add tribes and manage tribe membership.");

            webApplication.Uses(relationalDatabase, "Reads from and writes data to");
            webApplication.Uses(noSqlStore, "Reads from");
            webApplication.Uses(fileSystem, "Reads from");

            contentUpdater.Uses(relationalDatabase, "Reads from and writes data to");
            contentUpdater.Uses(noSqlStore, "Reads from and writes data to");
            contentUpdater.Uses(fileSystem, "Writes to");
            contentUpdater.Uses(twitter, "Gets profile information and recent tweets using the REST API from.",
                "JSON over HTTPS");
            contentUpdater.Uses(twitter, "Subscribes to tweets using the Twitter Streaming API from.", "JSON over HTTPS");
            contentUpdater.Uses(gitHub, "Gets information about public code repositories using the GitHub API from.",
                "JSON over HTTPS");
            contentUpdater.Uses(blogs, "Gets blog posts and news from.", "RSS and Atom over HTTP");

            // now create the system context view based upon the model
            ViewSet viewSet = workspace.views;
            SystemContextView contextView = viewSet.createContextView(techTribes);
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // and the container view
            ContainerView containerView = viewSet.createContainerView(techTribes);
            containerView.AddAllSoftwareSystems();
            containerView.AddAllPeople();
            containerView.addAllContainers();

            var componentView = viewSet.createComponentView(webApplication);
            componentView.addAllComponents();


            // and output the model and view to JSON
            Console.WriteLine(new JsonSerializer().Serialize(workspace, Formatting.Indented));

            // and upload the model to structurizr.com
            StructurizrClient structurizrClient = new StructurizrClient("https://api.structurizr.com", "api", "key");

            structurizrClient.PutWorkspace(workspace);
        }

    }
}