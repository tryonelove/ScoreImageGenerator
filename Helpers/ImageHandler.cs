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
            switch(_scoreType)
            {
                case ScoreType.Best: 
                    imageGenerator = new BestScoreImage(_username, _limit); break;
                case ScoreType.Last: 
                    imageGenerator = new RecentScoreImage(_username, _limit); break;
            }
            imageGenerator?.Generate();
            return;            
        }
    }
}