using System;
using ScoreImageGenerator.Generator.Objects;

namespace ScoreImageGenerator.Generator.Core
{
    public class BestScoreImage : ImageGenerator
    {
        public BestScoreImage(User user, Score score)
         : base(user, score)
        {
            Console.WriteLine("Generating Best score");
        }
    }
}