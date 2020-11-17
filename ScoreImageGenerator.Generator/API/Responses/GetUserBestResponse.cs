using System.Text.Json.Serialization;

namespace ScoreImageGenerator.Generator.API.Responses
{
    public class GetUserBestResponse: GetUserScore
    {

        [JsonPropertyName("score_id")]
        public string ScoreId { get; set; } 

        [JsonPropertyName("pp")]
        public string PP { get; set; } 

        [JsonPropertyName("replay_available")]
        public string ReplayAvailable { get; set; }
    }
}