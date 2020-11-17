using System;
using ScoreImageGenerator.Generator.Objects;

namespace ScoreImageGenerator.Generator.Core
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