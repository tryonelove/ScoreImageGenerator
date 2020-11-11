using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Objects
{
    public class Score
    {
        public int Mode { get ; set; }
        // Achieved score value
        public int ScoreValue { get; set;}
        // Enabled mods
        public int Mods { get; set; }
        // Rank achieved
        public string Rank { get; }
        // Count 300
        public int Count300 { get; }
        // Count 100
        public int Count100 { get; }
        // Count 50
        public int Count50 { get; }
        // Count misss
        public int CountMiss { get; }
        // Accuracy
        public float Accuracy { get => CalculateAccuracy() ; }
        // Achieved combo
        public int Combo { get; }
        // Beatmap stats
        public Beatmap Beatmap { get; }
        // PP for the score
        public double PP { get; set;  }

        public Score(GetUserBestResponse resp, Beatmap bmap)
        {
            Count300 = int.Parse(resp.Count300);
            Count100 = int.Parse(resp.Count100);
            Count50 = int.Parse(resp.Count50);
            CountMiss = int.Parse(resp.CountMiss);
            Mods = int.Parse(resp.EnabledMods ?? "0");
            Rank = resp.Rank;
            ScoreValue = int.Parse(resp.Score);
            Combo = int.Parse(resp.MaxCombo);
            Beatmap = bmap;
        }
        
        public Score(GetUserRecentResponse resp, Beatmap bmap)
        {
            Count300 = int.Parse(resp.Count300);
            Count100 = int.Parse(resp.Count100);
            Count50 = int.Parse(resp.Count50);
            CountMiss = int.Parse(resp.CountMiss);
            Mods = int.Parse(resp.EnabledMods ?? "0");
            Rank = resp.Rank;
            ScoreValue = int.Parse(resp.Score);
            Combo = int.Parse(resp.MaxCombo);
            Beatmap = bmap;
        }

        private float CalculateAccuracy()
        {
            float accuracy = (50f * Count50 + 100f * Count100 + 300f * Count300) / (300f *
                             (CountMiss + Count50 + Count100 + Count300));
            accuracy *= 100;
            return accuracy;
        }
    }
}