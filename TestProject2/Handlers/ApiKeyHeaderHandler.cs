using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TestProject2.Handlers
{
    public class ApiKeyHeaderHandler : DelegatingHandler
    {
        /// <summary>
        /// Name of our custom header to look for
        /// </summary>
        public const string _apiKeyHeader = "X-API-Key";

        /// <summary>
        ///  Name of api key query string key
        /// </summary>

        public const string _apiQueryString = "api_key";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // STEP 1: Global message-level logic that must be executed BEFORE the request
            //          is sent on to the action method

            //if (request.RequestUri.Segments[1].ToLowerInvariant().StartsWith("swagger"))

            //    return await base.SendAsync(request, cancellationToken);

            string apikey = null;

            if (request.Headers.Contains(_apiKeyHeader))
            {

                apikey = request.Headers.GetValues(_apiKeyHeader).FirstOrDefault();
            }
            else //lets see if its in the query string instead
            {

                var queryString = request.GetQueryNameValuePairs();
                var kvp = queryString.FirstOrDefault(a => a.Key.ToLowerInvariant().Equals(_apiQueryString));

                if (!string.IsNullOrEmpty(kvp.Value))
                {
                    apikey = kvp.Value;
                }
            }

            //was any api key present
            //if (string.IsNullOrEmpty(apikey))
            //{
            //    HttpResponseMessage res = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
            //    {
            //        Content = new StringContent("Missing Api Key")
            //    };

            //    return await Task.FromResult(res);
            //}


            request.Properties.Add(_apiKeyHeader, apikey);



            // STEP 2: Call the rest of the pipeline, all the way to a response message

           


            var response = await base.SendAsync(request, cancellationToken);



            // STEP 3: Any global message-level logic that must be executed AFTER the request
            //          has executed, before the final HTTP response message

           
            // STEP 4:  Return the final HTTP response
            return response;

        }

    }

    public static class HttprequestMessageApiKeyExtension
    {


        public static string GetApiKey(this HttpRequestMessage request)
        {

            if (request == null)
                return null;

            object value;
            if (request.Properties.TryGetValue(ApiKeyHeaderHandler._apiKeyHeader , out value))
            {

                return (string)value;
            }
            return null;
        }




    }
}