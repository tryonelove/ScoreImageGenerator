namespace ScoreImageGenerator
{
    public class BestScoreImage: IImageGenerator
    {
        string _username;
        int _limit;
        public BestScoreImage(string username, int limit)
        {
            _username = username;
            _limit = limit;
        }
        public void Generate()
        {
            return;
        }
    }
}