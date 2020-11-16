using System.IO;
using ScoreImageGenerator.Helpers;
using ScoreImageGenerator.Helpers.API.Responses;

namespace ScoreImageGenerator.Objects
{
    public class Beatmap
    {
        public int BeatmapSetId { get; set; }
        // Beatmap id
        public int BeatmapId { get; set; }
        // Beatmap title
        public string Title { get; set; }
        // Beatmap difficulty name
        public string DiffName { get; set; }
        // Circle size
        public float CS { get; set; }
        // Approach rate
        public float AR { get; set; }
        // Health point
        public float HP { get; set; }
        // Overall difficulty
        public float OD { get; set; }
        // Max beatmap combo
        public int MaxCombo { get; set; }
        // Full combo PP
        public double PP { get; set; }
        // Beatmap creator
        public string Creator { get; set; }
        // Song BPM
        public float BPM { get; set; }
        // Stars
        public float Stars { get; set; }
        public int Length { get; set; }

        public byte[] BackgroundImage { get; set; }
        
        public Beatmap(GetBeatmapsResponse bmap)
        {
            BeatmapSetId = int.Parse(bmap.BeatmapsetId ?? "0");
            BeatmapId = int.Parse(bmap.BeatmapId ?? "0");
            Title = bmap.Title;
            DiffName = bmap.Version;
            BPM = float.Parse(bmap.Bpm ?? "0");
            CS = float.Parse(bmap.DiffSize ?? "0");
            AR = float.Parse(bmap.DiffApproach ?? "0");
            HP = float.Parse(bmap.DiffDrain ?? "0");
            OD = float.Parse(bmap.DiffOverall ?? "0");
            MaxCombo = int.Parse(bmap.MaxCombo ?? "0");
            Creator = bmap.Creator;
            Stars = float.Parse(bmap.DifficultyRating ?? "0");
            Length = int.Parse(bmap.TotalLength ?? "0");
        }
    }
}