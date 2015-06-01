using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NStructurizr.Core.View;

namespace NStructurizr.Core.Client
{
    public class StructurizrClient
    {
        //private static final Log log = LogFactory.getLog(StructurizrClient.class);

        private static readonly String WORKSPACE_PATH = "/workspace/";

        private String url;
        private String apiKey;
        private String apiSecret;

       
        public StructurizrClient(String url, String apiKey, String apiSecret)
        {
            setUrl(url);
            this.apiKey = apiKey;
            this.apiSecret = apiSecret;
        }

        public String getUrl()
        {
            return url;
        }

        public void setUrl(String url)
        {
            if (url != null)
            {
                if (url.EndsWith("/"))
                {
                    this.url = url.Substring(0, url.Length - 1);
                }
                else
                {
                    this.url = url;
                }
            }
        }

        /**
         * Gets the workspace with the given ID.
         *
         * @param workspaceId   the ID of your workspace
         * @return a Workspace instance
         * @throws Exception    if there are problems related to the network, authorization, JSON deserialization, etc
         */
        //public Workspace getWorkspace(long workspaceId) throws Exception {
        //    CloseableHttpClient httpClient = HttpClients.createDefault();
        //    HttpGet httpGet = new HttpGet(url + WORKSPACE_PATH + workspaceId);
        //    addHeaders(httpGet, "", "");
        //    debugRequest(httpGet, "");

        //    try (CloseableHttpResponse response = httpClient.execute(httpGet)) {
        //        debugResponse(response);

        //        String json = EntityUtils.toString(response.getEntity());
        //        if (response.getStatusLine().getStatusCode() == HttpStatus.SC_OK) {
        //            return new JsonReader().read(new StringReader(json));
        //        } else {
        //            ApiError apiError = ApiError.parse(json);
        //            throw new StructurizrClientException(apiError.getMessage());
        //        }
        //    }
        //}

        public class Properties
        {
        }

        /**
         * Updates the given workspace.
         *
         * @param workspace     the workspace instance to update
         * @throws Exception    if there are problems related to the network, authorization, JSON serialization, etc
         */
        public void putWorkspace(Workspace workspace)
        {
            if (workspace == null)
            {
                throw new ArgumentException("A workspace must be supplied");
            }
            else if (workspace.id <= 0)
            {
                throw new ArgumentException("The workspace ID must be set");
            }

            var settings = new JsonSerializerSettings()
            {
                Converters = { new StringEnumConverter(), new PaperSizeJsonConverter() },
                NullValueHandling = NullValueHandling.Ignore
            };

            var workspaceJson = JsonConvert.SerializeObject(workspace, settings);
            var putUrl = url + WORKSPACE_PATH + workspace.id;

            using (var client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
                addHeaders(client, workspaceJson, "application/json", new Uri(putUrl).AbsolutePath);

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
                }
            }
        }

        /**
         * Fetches the workspace with the given workspaceId from the server and merges its layout information with
         * the given workspace. All models from the the new workspace are taken, only the old layout information is preserved.
         *
         * @param workspaceId   the ID of your workspace
         * @param workspace     the new workspace
         * @throws Exception    if you are not allowed to update the workspace with the given ID or there are any network troubles
         */
        //public void mergeWorkspace(long workspaceId, Workspace workspace) throws Exception {
        //    Workspace currentWorkspace = getWorkspace(workspaceId);
        //    if (currentWorkspace != null) {
        //        workspace.getViews().copyLayoutInformationFrom(currentWorkspace.getViews());
        //    }
        //    workspace.setId(workspaceId);
        //    putWorkspace(workspace);
        //}

        //private void debugRequest(HttpRequestBase httpRequest, String content) {
        //    log.debug(httpRequest.getMethod() + " " + httpRequest.getURI().getPath());
        //    Header[] headers = httpRequest.getAllHeaders();
        //    for (Header header : headers) {
        //        log.debug(header.getName() + ": " + header.getValue());
        //    }

        //    log.debug(content);
        //}

        //private void debugResponse(CloseableHttpResponse response) {
        //    log.info(response.getStatusLine());
        //}

        private void addHeaders(WebClient client, String content, string contentType, string urlPath)
        {
            String httpMethod = "PUT";
            String path = urlPath;
            String contentMd5 = new Md5Digest().generate(content);
            String nonce = "" + CurrentTimeMillis();

            HashBasedMessageAuthenticationCode hmac = new HashBasedMessageAuthenticationCode(apiSecret);
            HmacContent hmacContent = new HmacContent(httpMethod, path, contentMd5, contentType, nonce);

            client.Headers.Add(HttpRequestHeader.Authorization, new HmacAuthorizationHeader(apiKey, hmac.generate(hmacContent.ToString())).format());
            client.Headers.Add("Nonce", nonce);
            client.Headers.Add("Content-MD5", Convert.ToBase64String(Encoding.UTF8.GetBytes(contentMd5)));
        }

        private static readonly DateTime Jan1st1970 = new DateTime
            (1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long CurrentTimeMillis()
        {
            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

    }
}