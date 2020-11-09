


using System;
using ScoreImageGenerator.Objects;

namespace ScoreImageGenerator.Helpers
{
    public class RecentScoreImage : ImageGenerator
    {
        public RecentScoreImage(User user, Score score)
         : base(user, score)
        {
            Console.WriteLine("Generating Recent score");
        }
    }
}