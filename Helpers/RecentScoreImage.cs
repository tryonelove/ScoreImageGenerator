


using System;
using ScoreImageGenerator.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ScoreImageGenerator
{
    public class RecentScoreImage : ImageGenerator
    {
        public RecentScoreImage(User user, Score score)
         : base(user, score, ScoreType.Last)
        {
            Console.WriteLine("Recent score");
        }
    }
}