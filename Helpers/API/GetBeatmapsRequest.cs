using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Helpers.API
{
    public class GetBeatmapsRequest : Request<GetBeatmapsResponse>
    {
        protected override string Endpoint => "get_beatmaps";

        private readonly string _username;
        private readonly int _beatmapSetId;
        private readonly int _beatmapId;
        private readonly int _mode;
        private readonly int _limit;
        
        public GetBeatmapsRequest(string username = "", int s = 0, int b = 0, int m = 0, int limit = 100)
        {
            _username = username;
            _beatmapSetId = s;
            _beatmapId = b;
            _mode = m;
            _limit = limit;
        }

        protected override Uri BuildUri()
        {
            var builder = Builder;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["k"] = Environment.GetEnvironmentVariable("OSU_API_KEY");
            if (!string.IsNullOrEmpty(_username)) 
                parameters["u"] = _username;
            if (_beatmapSetId != 0) 
                parameters["s"] = _beatmapSetId.ToString();
            if (_beatmapId != 0) 
                parameters["b"] = _beatmapId.ToString();
            parameters["m"] = _mode.ToString() ?? "0";
            parameters["limit"] = _limit.ToString() ?? "0";
            builder.Query = parameters.ToString() ?? string.Empty;
            return builder.Uri;
        }
    }
}