using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ScoreImageGenerator.Helpers.API
{
    public class APIAccess<T>
    {
        private string _key;
        private static HttpClient client = new HttpClient();
        public string Endpoint = "https://osu.ppy.sh/api";
        private T apiObject;
        // public APIAccess(string key)
        // {
        //     _key = key;
        // }

        public async Task<T> PerformAsync()
        {
            HttpResponseMessage response = await client.GetAsync("https://osu.ppy.sh/api/get_user_recent?k=09aaab69d2ecc40028579ca5f7fe2d58b1653e6e&u=rafis");
            if(response.IsSuccessStatusCode)
            {
                using(Stream responseStream= await response.Content.ReadAsStreamAsync())
                {
                    apiObject = await JsonSerializer.DeserializeAsync<T>(responseStream);
                }
            }
            return apiObject;
        }
    }
}