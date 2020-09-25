using ScoreImageGenerator.Helpers;

namespace ScoreImageGenerator
{
    public class ImageHandler
    {
        string _username;
        int _limit;
        ScoreType _scoreType;
        public ImageHandler(string username, int limit, ScoreType scoreType)
        {
            _username = username;
            _limit = limit;
            _scoreType = scoreType;
        }

        public void GetImage()
        {
            IImageGenerator imageGenerator = null;

            User user = new User();
            user.Username = _username;

            Score score = new Score();

            switch(_scoreType)
            {
                case ScoreType.Best: 
                    imageGenerator = new BestScoreImage(user, score); break;
                case ScoreType.Last: 
                    imageGenerator = new RecentScoreImage(user, score); break;
            }
            imageGenerator?.Generate();
            return;            
        }
    }
}