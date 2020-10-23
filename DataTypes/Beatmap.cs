namespace ScoreImageGenerator.Helpers
{
    public class Beatmap
    {
        // Beatmap id
        public int Id { get; set; }
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
        public float OD { get; set; }
        public float BPM { get; set; }
        public float Stars { get; set; }
        // Max beatmap combo
        public int MaxCombo { get; set; }
        // Full combo PP
        public float PP { get; set; }
        public int Length { get; set; }
        // Beatmap creator
        public string Creator { get; set; }
    }
}