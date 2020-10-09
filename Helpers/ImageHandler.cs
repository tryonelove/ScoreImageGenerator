using ScoreImageGenerator.Helpers;

namespace ScoreImageGenerator.Helpers
{
    public class ImageHandler
    {
        string _username;
        int _limit;
        ScoreType _scoreType;
        public ImageHandler(string username, int limit, int scoreType)
        {
            _username = username;
            _limit = limit;
            _scoreType = (ScoreType)scoreType;
        }

        public void GetImage()
        {
            ImageGenerator imageGenerator = null;

            User user = new User();
            user.Username = _username;
            Score score = new Score();
            Beatmap bmap = new Beatmap();
            
            // Set bmap stats
            bmap.AR = 5;
            bmap.CS = 5;
            bmap.DiffName = "Bright & Cheerful";
            bmap.HP = 5;
            bmap.Id = 3228;
            bmap.MaxCombo = 322;
            bmap.PP = 3222.01f;
            bmap.Title = "mimimemeMIMI - Harebare Fanfare";
            bmap.Creator = "Luna-";
            // Set score
            score.Count300 = 300;
            score.Count100 = 100;
            score.Count50 = 50;
            score.CountMiss = 0;
            score.Mods = 72;
            score.PP = 320.01f;
            score.Rank = "A";
            score.ScoreValue = 1337322;
            score.Beatmap = bmap;

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