using System;
using System.Collections.Specialized;
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
        
        protected override NameValueCollection GetRequestParameters(NameValueCollection parameters)
        {
            parameters["u"] = _username;
            parameters["m"] = _mode.ToString();
            parameters["limit"] = _limit.ToString();
            return parameters;
        }
    }
}