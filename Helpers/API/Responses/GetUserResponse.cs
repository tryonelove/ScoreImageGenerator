using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ScoreImageGenerator.Helpers.API.Responses
{
    public class OsuEvent
    {
        [JsonPropertyName("display_html")]
        public string DisplayHtml { get; set; } 

        [JsonPropertyName("beatmap_id")]
        public string BeatmapId { get; set; } 

        [JsonPropertyName("beatmapset_id")]
        public string BeatmapsetId { get; set; } 

        [JsonPropertyName("date")]
        public string Date { get; set; } 

        [JsonPropertyName("epicfactor")]
        public string Epicfactor { get; set; } 
    }
    
    public class GetUserResponse
    {
        [JsonPropertyName("user_id")]
        public string UserId { get; set; } 

        [JsonPropertyName("username")]
        public string Username { get; set; } 

        [JsonPropertyName("join_date")]
        public string JoinDate { get; set; } 

        [JsonPropertyName("count300")]
        public string Count300 { get; set; } 

        [JsonPropertyName("count100")]
        public string Count100 { get; set; } 

        [JsonPropertyName("count50")]
        public string Count50 { get; set; } 

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; } 

        [JsonPropertyName("ranked_score")]
        public string RankedScore { get; set; } 

        [JsonPropertyName("total_score")]
        public string TotalScore { get; set; } 

        [JsonPropertyName("pp_rank")]
        public string PpRank { get; set; } 

        [JsonPropertyName("level")]
        public string Level { get; set; } 

        [JsonPropertyName("pp_raw")]
        public string PpRaw { get; set; } 

        [JsonPropertyName("accuracy")]
        public string Accuracy { get; set; } 

        [JsonPropertyName("count_rank_ss")]
        public string CountRankSs { get; set; } 

        [JsonPropertyName("count_rank_ssh")]
        public string CountRankSsh { get; set; } 

        [JsonPropertyName("count_rank_s")]
        public string CountRankS { get; set; } 

        [JsonPropertyName("count_rank_sh")]
        public string CountRankSh { get; set; } 

        [JsonPropertyName("count_rank_a")]
        public string CountRankA { get; set; } 

        [JsonPropertyName("country")]
        public string Country { get; set; } 

        [JsonPropertyName("total_seconds_played")]
        public string TotalSecondsPlayed { get; set; } 

        [JsonPropertyName("pp_country_rank")]
        public string PpCountryRank { get; set; } 

        [JsonPropertyName("events")]
        public List<OsuEvent> Events { get; set; } 
    }
}