using System.Text.Json.Serialization;

namespace ScoreImageGenerator.Generator.API.Responses
{
    public class GetUserScore
    {
        [JsonPropertyName("beatmap_id")]
        public string BeatmapId { get; set; } 
        
        [JsonPropertyName("score")]
        public string Score { get; set; } 

        [JsonPropertyName("maxcombo")]
        public string MaxCombo { get; set; } 

        [JsonPropertyName("count50")]
        public string Count50 { get; set; } 

        [JsonPropertyName("count100")]
        public string Count100 { get; set; } 

        [JsonPropertyName("count300")]
        public string Count300 { get; set; } 

        [JsonPropertyName("countmiss")]
        public string CountMiss { get; set; } 

        [JsonPropertyName("countkatu")]
        public string CountKatu { get; set; } 

        [JsonPropertyName("countgeki")]
        public string CountGeki { get; set; } 

        [JsonPropertyName("perfect")]
        public string Perfect { get; set; } 

        [JsonPropertyName("enabled_mods")]
        public string EnabledMods { get; set; } 

        [JsonPropertyName("user_id")]
        public string UserId { get; set; } 

        [JsonPropertyName("date")]
        public string Date { get; set; } 

        [JsonPropertyName("rank")]
        public string Rank { get; set; } 
    }
}