using System;
using System.IO;
using System.Net;
using System.Text;
using NStructurizr.Core;

namespace NStructurizr.Client
{
    public class StructurizrClient
    {
        private static readonly string WORKSPACE_PATH = "/workspace/";
        private static readonly DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private string url;
        private string apiKey;
        private string apiSecret;

        public StructurizrClient()
        {
            ReadFromAppConfig();
        }

        private void ReadFromAppConfig()
        {
            
        }

        public StructurizrClient(string url, string apiKey, string apiSecret)
        {
            Url = url;
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public string Url
        {
            get { return this.url; }
            set
            {
                if (value == null) return;

                if (value.EndsWith("/"))
                    this.url = value.Substring(0, value.Length - 1);
                else
                    this.url = value;
            }
        }

        /// <summary>Gets the workspace with the given ID.</summary>
        /// <param name="workspaceId">the ID of your workspace</param>
        /// <returns>a Workspace instance</returns>
        /// <exception>if there are problems related to the network, authorization, JSON deserialization, etc</exception>
        public Workspace GetWorkspace(long workspaceId) 
        {
            using (var client = new WebClient())
            {
                var getUrl = url + WORKSPACE_PATH + workspaceId;

                AddHeaders(client, string.Empty, string.Empty, new Uri(getUrl).AbsolutePath, "GET");

                try
                {
                    var response = client.DownloadString(getUrl);
                    Console.WriteLine(response);
                    return new JsonSerializer().Deserialize(response);
                }
                catch (WebException e)
                {
                    using (var streamReader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        Console.WriteLine(streamReader.ReadToEnd());
                    }

                    throw;
                }
            }
        }
       
        /// <summary>Updates the given workspace.</summary>
        /// <param name="workspace">the workspace instance to update</param>
        /// <exception>if there are problems related to the network, authorization, JSON serialization, etc</exception>
        public void PutWorkspace(Workspace workspace)
        {
            if (workspace == null)
                throw new ArgumentException("A workspace must be supplied");
            else if (workspace.id <= 0)
                throw new ArgumentException("The workspace ID must be set");

            var workspaceJson = new JsonSerializer().Serialize(workspace);
            var putUrl = url + WORKSPACE_PATH + workspace.id;

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                AddHeaders(client, workspaceJson, "application/json", new Uri(putUrl).AbsolutePath, "PUT");

                try
                {
                    client.UploadString(putUrl, "PUT", workspaceJson);
                }
                catch (WebException e)
                {
                    using (var streamReader = new StreamReader(e.Response.GetResponseStream()))
                    {
                        Console.WriteLine(streamReader.ReadToEnd());
                    }

                    throw;
                }
            }
        }

       
        /// <summary>
        /// Fetches the workspace with the given workspaceId from the server and merges its layout information with
        /// the given workspace. All models from the the new workspace are taken, only the old layout information is preserved.
        /// </summary>
        /// <param name="workspaceId">the ID of your workspace</param>
        /// <param name="workspace">the new workspace</param>
        /// <exception>if you are not allowed to update the workspace with the given ID or there are any network troubles</exception>
        public void MergeWorkspace(long workspaceId, Workspace workspace)
        {
            Workspace currentWorkspace = GetWorkspace(workspaceId);
            if (currentWorkspace != null)
                workspace.views.copyLayoutInformationFrom(currentWorkspace.views);

            workspace.id = workspaceId;
            PutWorkspace(workspace);
        }

        private void AddHeaders(WebClient client, string content, string contentType, string urlPath, string method)
        {
            string httpMethod = method;
            string path = urlPath;
            string contentMd5 = new Md5Digest().Generate(content);
            string nonce = CurrentTimeMillis().ToString();

            var hmac = new HashBasedMessageAuthenticationCode(apiSecret);
            var hmacContent = new HmacContent(httpMethod, path, contentMd5, contentType, nonce);

            client.Headers.Add(HttpRequestHeader.Authorization, new HmacAuthorizationHeader(apiKey, hmac.Generate(hmacContent.ToString())).Format());
            client.Headers.Add("Nonce", nonce);
            client.Headers.Add("Content-MD5", Convert.ToBase64String(Encoding.UTF8.GetBytes(contentMd5)));
        }

        private static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

    }
}