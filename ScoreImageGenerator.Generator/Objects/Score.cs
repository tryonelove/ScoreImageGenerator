using System;
using System.Diagnostics;
using ScoreImageGenerator.Generator.API.Responses;

namespace ScoreImageGenerator.Generator.Objects
{
    public class Score: ICloneable
    {
        public Mode Mode { get; set; }

        // Achieved score value
        public int ScoreValue { get; set;}

        // Enabled mods
        public int Mods { get; set; }
 
        // Rank achieved
        public string Rank { get; }

        // Count 300
        public int Count300 { get; set;  }

        // Count 100
        public int Count100 { get; set;  }

        // Count 50
        public int Count50 { get; set;  }
        // Count misss
        public int CountMiss { get; set;  }

        public int CountGeki { get; set; }

        public int CountKatu { get; set; }

        // Accuracy
        public double Accuracy {
            get
            {
                return 100 * Mode switch
                {
                    Mode.Osu => CalculateStdAccuracy(),
                    Mode.Taiko => CalculateTaikoAccuracy(),
                    Mode.Catch => CalculateCtbAccuracy(),
                    Mode.Mania => CalculateManiaAccuracy(),
                    _ => 0
                };
            }
        }

        // Achieved combo
        public int Combo { get; set;  }

        // Beatmap stats
        public Beatmap Beatmap { get; }

        // PP for the score
        public double PP { get; set;  }
        
        public Score(GetUserScore resp, Beatmap bmap)
        {
            Count300 = int.Parse(resp.Count300);
            Count100 = int.Parse(resp.Count100);
            Count50 = int.Parse(resp.Count50);
            CountGeki = int.Parse(resp.CountGeki);
            CountKatu = int.Parse(resp.CountKatu);
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
            CountGeki = int.Parse(resp.CountGeki);
            CountKatu = int.Parse(resp.CountKatu);
            Mods = int.Parse(resp.EnabledMods ?? "0");
            Rank = resp.Rank;
            ScoreValue = int.Parse(resp.Score);
            Combo = int.Parse(resp.MaxCombo);
            Beatmap = bmap;
        }
        
        private double CalculateStdAccuracy()
        {
            return (50f * Count50 + 100f * Count100 + 300f * Count300) / 
                   (300f * (CountMiss + Count50 + Count100 + Count300));
        }

        private double CalculateTaikoAccuracy()
        {
            return (0.5 * Count100 + Count300) / (CountMiss + Count50 + Count100 + Count300);
        }

        private double CalculateCtbAccuracy()
        {
            return ((double)Count50 + Count100 + Count300) / (Count50 + Count100 + Count300 + CountMiss + CountKatu);
        }

        private double CalculateManiaAccuracy()
        {
            return (Count50 * 50f + Count100 * 100f + CountKatu * 200f + (CountGeki + Count300) * 300f) / ((Count50 + Count100 + Count300 + CountMiss + CountGeki + CountKatu) * 300f);
        }

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}