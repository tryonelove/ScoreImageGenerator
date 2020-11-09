using System.IO;
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
        public float PP { get; set; }
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
            BeatmapSetId = int.Parse(bmap.BeatmapsetId);
            BeatmapId = int.Parse(bmap.BeatmapId);
            Title = bmap.Title;
            DiffName = bmap.Version;
            CS = float.Parse(bmap.DiffSize);
            AR = float.Parse(bmap.DiffApproach);
            HP = float.Parse(bmap.DiffDrain);
            OD = float.Parse(bmap.DiffOverall);
            MaxCombo = int.Parse(bmap.MaxCombo);
            PP = 322f;
            Creator = bmap.Creator;
            // BPM = int.Parse(bmap.BPM);
            Stars = float.Parse(bmap.DifficultyRating);
            Length = int.Parse(bmap.TotalLength);
        }
    }
}