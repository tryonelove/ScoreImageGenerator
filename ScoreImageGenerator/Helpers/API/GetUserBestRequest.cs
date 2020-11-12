using System;
using System.Web;
using ScoreImageGenerator.Objects;

namespace ScoreImageGenerator.Helpers.API.Responses
{
    public class GetUserBestRequest : Request<GetUserBestResponse>
    {
        protected override string Endpoint => "get_user_best";

        private readonly string _username;
        private readonly int _mode;
        private readonly int _limit;

        public GetUserBestRequest(string username, Mode mode, int limit)
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