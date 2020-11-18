using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace ScoreImageGenerator.Generator.API
{
    public abstract class Request<T>
    {
        /// <summary>
        /// Base API URI
        /// </summary>
        private const string BaseUri = "https://osu.ppy.sh/api/";

        /// <summary>
        /// API endpoint.
        /// </summary>
        protected abstract string Endpoint { get; }

        /// <summary>
        /// Uri builder
        /// </summary>
        private UriBuilder Builder => new UriBuilder(BaseUri + Endpoint);

        private readonly HttpClient _client = new HttpClient();

        /// <summary>
        /// Get request uri parameters
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns><c>NameValueCollection</c> of parameters</returns>
        protected abstract NameValueCollection GetRequestParameters(NameValueCollection parameters);
        
        /// <summary>
        /// Build Uri to request.
        /// </summary>
        /// <returns>Uri string with query params.</returns>
        private Uri BuildUri()
        {
            UriBuilder builder = Builder;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["k"] = Environment.GetEnvironmentVariable("OSU_API_KEY");
            parameters = GetRequestParameters(parameters);
            builder.Query = parameters.ToString() ?? string.Empty;
            return builder.Uri;
        }

        /// <summary>
        /// Make an async request to API.
        /// </summary>
        /// <returns>List of requested objects</returns>
        public async Task<List<T>> PerformAsync()
        {
            var uri = BuildUri();
            var response = await _client.GetAsync(uri);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpListenerException((int)response.StatusCode);
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            if (responseStream is null)
            {
                throw new EndOfStreamException($"Response stream is null.");
            }

            return await JsonSerializer.DeserializeAsync<List<T>>(responseStream);;
        }
    }
}