using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScoreImageGenerator.Helpers.API.Responses
{
    public class GetUserRecentResponse: GetUserScore
    {
        [JsonPropertyName("beatmap_id")]
        public string BeatmapId { get; set; }
    }
}