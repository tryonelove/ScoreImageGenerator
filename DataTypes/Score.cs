namespace ScoreImageGenerator.Helpers
{
    public class Score
    {
        // Achieved score value
        public int ScoreValue { get; set;}
        // Enabled mods
        public int Mods { get; set; }
        // Rank achieved
        public string Rank { get; set; }
        // Count 300
        public int Count300 { get; set; }
        // Count 100
        public int Count100 { get; set; }
        // Count 50
        public int Count50 { get; set; }
        // Count misss
        public int CountMiss { get; set; }
        public float Accuracy { get; set; }
        // Achieved combo
        public int Combo { get; set; }
        // Beatmap stats
        public Beatmap Beatmap { get; set;}
        // PP for the score
        public float PP { get; set; }
    }
}