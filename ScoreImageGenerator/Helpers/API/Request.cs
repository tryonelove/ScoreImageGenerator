using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;


namespace ScoreImageGenerator.Helpers.API
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
        protected UriBuilder Builder => new UriBuilder(BaseUri + Endpoint);
        
        /// <summary>
        /// Build Uri to request.
        /// </summary>
        /// <returns>Uri string with query params.</returns>
        protected abstract Uri BuildUri();

        /// <summary>
        /// Make an async request to API.
        /// </summary>
        /// <returns>List of requested objects</returns>
        public async Task<List<T>> PerformAsync()
        {
            var uri = BuildUri();
            var request =  (HttpWebRequest)WebRequest.Create(uri);
            request.Accept = "application/json";
            var response = (HttpWebResponse) await request.GetResponseAsync();
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpListenerException((int)response.StatusCode);
            }

            var responseStream = response.GetResponseStream();
            if (responseStream is null)
            {
                throw new EndOfStreamException($"Response stream is null.");
            }
            
            List<T> deserialized = await JsonSerializer.DeserializeAsync<List<T>>(responseStream);
            return deserialized;
        }
    }
}