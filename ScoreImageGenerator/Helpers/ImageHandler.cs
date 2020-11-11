using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScoreImageGenerator.Helpers.API.Responses;
using ScoreImageGenerator.Helpers.API;
using ScoreImageGenerator.Objects;
using SixLabors.ImageSharp;

namespace ScoreImageGenerator.Helpers
{
    public class ImageHandler
    {
        private readonly string _username;
        private readonly int _limit;
        private readonly OsuMode _mode;
        private readonly ScoreType _scoreType;

        public ImageHandler(string username, int limit, int mode, int scoreType)
        {
            _username = username;
            _limit = limit;
            _mode = (OsuMode)mode;
            _scoreType = (ScoreType)scoreType;
        }

        private Score GetBestScore(User user)
        {
            var userRequest = new GetUserBestRequest(user.Username, _mode, 0);
            var userBestResponses = userRequest.PerformAsync().Result;
            GetUserBestResponse resp = userBestResponses[0];

            var bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1, m: _mode);
            var bmapResponse = bmapRequest.PerformAsync().Result;
            if (bmapResponse.Count == 0)
                throw new ArgumentException();
            
            GetBeatmapsResponse bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };
            
            return new Score(resp, bmap);
        }

        private Score GetRecentScore(User user)
        {
            var request = new GetUserRecentRequest(user.Username, _mode, 0);
            var response = request.PerformAsync().Result;
            if (response.Count == 0)
                throw new ArgumentException();
            
            GetUserRecentResponse resp = response.First();
            
            var bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1);
            var bmapResponse = bmapRequest.PerformAsync().Result;
            if (bmapResponse.Count == 0)
                throw new ArgumentException();
            
            GetBeatmapsResponse bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };
            
            return new Score(resp, bmap);
        }

        private User GetUser(string username, OsuMode m = OsuMode.osu)
        {
            var request = new GetUserRequest(username, m);
            var response = request.PerformAsync().Result;
            GetUserResponse resp = response[0];
            User user = new User(resp)
            {
                Avatar = Utils.GetUserAvatar(resp.UserId)
            };

            return user;
        }
        
        public byte[] GetImage()
        {
            var user = GetUser(_username, _mode);
            var score = _scoreType switch
            {
                ScoreType.Best => GetBestScore(user),
                ScoreType.Recent => GetRecentScore(user),
                _ => null
            };

            List<string> mods = Utils.GetModsList(score.Mods);
            
            PpCalculator pp = new PpCalculator(score.Beatmap.BeatmapId);
            
            score.Beatmap.PP = pp.GetFcPp(mods, _mode).Pp;
            score.PP = pp.GetScorePp(score, mods, _mode).Pp;
            
            ImageGenerator imageGenerator = _scoreType switch
            {
                ScoreType.Best => new BestScoreImage(user, score),
                ScoreType.Recent => new RecentScoreImage(user, score),
                _ => null
            };
            Image image;
            byte[] imageArray;
            try
            {
                image = imageGenerator?.Generate();
            }
            catch (Exception e)
            {
                image = Image.LoadAsync("./Static/usernotfound.png").Result;
            }
           
            using var ms = new MemoryStream();
            image.SaveAsPng(ms);
            imageArray = ms.GetBuffer();
            
            return imageArray;
        }
    }
}