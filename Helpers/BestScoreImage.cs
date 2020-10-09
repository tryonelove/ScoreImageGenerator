using System;
using ScoreImageGenerator.Helpers;

namespace ScoreImageGenerator.Helpers
{
    public class BestScoreImage : ImageGenerator
    {
        public BestScoreImage(User user, Score score)
         : base(user, score, ScoreType.Best)
        {
            Console.WriteLine("Best score");
        }
    }
}