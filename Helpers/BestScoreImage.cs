using System;
using ScoreImageGenerator.Helpers.API;
using ScoreImageGenerator.Helpers.API.Responses;
using ScoreImageGenerator.Objects;


namespace ScoreImageGenerator.Helpers
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