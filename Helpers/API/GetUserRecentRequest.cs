using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ScoreImageGenerator.Helpers.API.Responses;
using ScoreImageGenerator.Objects;

namespace ScoreImageGenerator.Helpers.API
{
    public class GetUserRecentRequest : Request<GetUserRecentResponse>
    {
        protected override string Endpoint => "get_user_recent";

        private readonly string _username;
        private readonly int _mode;
        private readonly int _limit;

        public GetUserRecentRequest(string username, OsuMode mode, int limit)
        {
            _username = username;
            _mode = (int)mode;
            _limit = limit;
        }
        
        protected override Uri BuildUri()
        {
            var builder = Builder;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["k"] = Environment.GetEnvironmentVariable("OSU_API_KEY");
            parameters["u"] = _username;
            parameters["m"] = _mode.ToString();
            parameters["limit"] = _limit.ToString();
            builder.Query = parameters.ToString() ?? string.Empty;
            return builder.Uri;
        }
    }
}