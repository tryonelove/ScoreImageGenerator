using System.Collections.Specialized;
using ScoreImageGenerator.Helpers.API.Responses;
using ScoreImageGenerator.Objects;

namespace ScoreImageGenerator.Helpers.API
{
    public class GetUserRequest : Request<GetUserResponse>
    {
        protected override string Endpoint => "get_user";
        private readonly string _username;
        private readonly int _mode;
        
        public GetUserRequest(string u, Mode mode = 0)
        {
            _username = u;
            _mode = (int)mode;
        }

        protected override NameValueCollection GetRequestParameters(NameValueCollection parameters)
        {
            parameters["u"] = _username;
            parameters["m"] = _mode.ToString();
            return parameters;
        }
    }
}