using System;

namespace ScoreImageGenerator.Helpers.API.Responses
{
    public class GetUserRecentResponse
    {
        public int BeatmapId { get; set; }
        public int Score { get; set; }
        public int MaxCombo { get; set; }
        public int Count300 { get; set; }
        public int Count100 { get; set; }
        public int Count50 { get; set; }
        public int CountMiss { get; set; }
        public int CountKatu { get; set; }
        public int CountGeki { get; set; }
        public bool Perfect { get; set; }
        public int EnabledMods { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Rank { get; set; }
    }
}