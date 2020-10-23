


using System;
using ScoreImageGenerator.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ScoreImageGenerator.Helpers
{
    public class RecentScoreImage : ImageGenerator
    {
        public RecentScoreImage(User user, Score score)
         : base(user, score, ScoreType.Recent)
        {
            Console.WriteLine("Recent score");
        }
    }
}