using System;
using System.Web;
using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Helpers.API
{
    public class GetUserRequest : Request<GetUserResponse>
    {
        protected override string Endpoint => "get_user";
        private readonly string _username;
        private readonly int _mode;
        
        public GetUserRequest(string u, int m = 0)
        {
            _username = u;
            _mode = m;
        }
        
        protected override Uri BuildUri()
        {
            var builder = Builder;
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["k"] = Environment.GetEnvironmentVariable("OSU_API_KEY");
            parameters["u"] = _username;
            parameters["m"] = _mode.ToString();
            builder.Query = parameters.ToString() ?? string.Empty;
            return builder.Uri;
        }
    }
}