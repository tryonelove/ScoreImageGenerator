using System.Collections.Specialized;
using ScoreImageGenerator.Generator.API.Responses;
using ScoreImageGenerator.Generator.Objects;

namespace ScoreImageGenerator.Generator.API
{
    public class GetBeatmapsRequest : Request<GetBeatmapsResponse>
    {
        protected override string Endpoint => "get_beatmaps";

        private readonly string _username;
        private readonly int _beatmapSetId;
        private readonly int _beatmapId;
        private readonly int _mode;
        private readonly int _limit;
        
        public GetBeatmapsRequest(string username = "", int s = 0, int b = 0, Mode m = 0, int limit = 100)
        {
            _username = username;
            _beatmapSetId = s;
            _beatmapId = b;
            _mode = (int)m;
            _limit = limit;
        }
        
        protected override NameValueCollection GetRequestParameters(NameValueCollection parameters)
        {
            if (!string.IsNullOrEmpty(_username)) 
                parameters["u"] = _username;
            if (_beatmapSetId != 0) 
                parameters["s"] = _beatmapSetId.ToString();
            if (_beatmapId != 0) 
                parameters["b"] = _beatmapId.ToString();
            parameters["m"] = _mode.ToString();
            parameters["limit"] = _limit.ToString();
            return parameters;
        }
    }
}