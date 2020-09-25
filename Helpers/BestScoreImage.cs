using ScoreImageGenerator.Helpers;

namespace ScoreImageGenerator
{
    public class BestScoreImage: IImageGenerator
    {
        User _user;
        Score _score;
        public BestScoreImage(User user, Score score)
        {
            _user = user;
            _score = score;
        }
        public void Generate()
        {
            return;
        }
    }
}