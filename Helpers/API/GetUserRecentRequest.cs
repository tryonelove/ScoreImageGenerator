using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Helpers.API
{
    public class GetUserRecent : IRequest<GetUserRecentResponse>
    {
        public string Endpoint => "get_user_recent";

        private readonly string _username;
        private readonly string _mode;
        private readonly string _limit;

        public GetUserRecent(string username, string mode, string limit)
        {
            _username = username;
            _mode = mode;
            _limit = limit;
        }
        
        public Uri BuildUri()
        {
            var builder = new UriBuilder("https://osu.ppy.sh/api/" + Endpoint);
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["k"] = Environment.GetEnvironmentVariable("OSU_API_KEY");
            parameters["u"] = _username;
            parameters["m"] = _mode.ToString();
            parameters["limit"] = _limit.ToString();
            builder.Query = parameters.ToString() ?? string.Empty;
            return builder.Uri;
        }

        public Task<List<GetUserRecentResponse>> PerformAsync()
        {
            
        }
    }
}