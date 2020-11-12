using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
        private readonly Mode _mode;
        private readonly ScoreType _scoreType;

        public ImageHandler(string username, int limit, int mode, int scoreType)
        {
            _username = username;
            _limit = limit;
            _mode = (Mode)mode;
            _scoreType = (ScoreType)scoreType;
        }

        private Score GetBestScore(User user)
        {
            GetUserBestRequest userRequest = new GetUserBestRequest(user.Username, _mode, 0);
            List<GetUserBestResponse> userBestResponses = userRequest.PerformAsync().Result;
            if (userBestResponses.Count == 0)
            {
                return null;
            }

            GetUserBestResponse resp = userBestResponses.First();

            GetBeatmapsRequest bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1, m: _mode);
            List<GetBeatmapsResponse> bmapResponse = bmapRequest.PerformAsync().Result;
            if (bmapResponse.Count == 0)
            {
                return null;
            }

            GetBeatmapsResponse bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };
            
            return new Score(resp, bmap) { Mode = _mode};
        }

        private Score GetRecentScore(User user)
        {
            var request = new GetUserRecentRequest(user.Username, _mode, 0);
            var response = request.PerformAsync().Result;
            if (response.Count == 0)
            {
                return null;
            }

            GetUserRecentResponse resp = response.First();
            
            var bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1);
            var bmapResponse = bmapRequest.PerformAsync().Result;
            if (bmapResponse.Count == 0)
            {
                return null;
            }

            GetBeatmapsResponse bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };
            
            return new Score(resp, bmap) { Mode = _mode};
        }

        private User GetUser(string username, Mode m = Mode.osu)
        {
            GetUserRequest request = new GetUserRequest(username, m);
            List<GetUserResponse> getUserResponses = request.PerformAsync().Result;
            GetUserResponse getUserResponse = getUserResponses.First();
            User user = new User(getUserResponse)
            {
                Avatar = Utils.GetUserAvatar(getUserResponse.UserId)
            };

            return user;
        }
        
        public byte[] GetImage()
        {
            Image image;

            var user = GetUser(_username, _mode);
            var score = _scoreType switch
            {
                ScoreType.Best => GetBestScore(user),
                ScoreType.Recent => GetRecentScore(user),
                _ => null
            };

            if (score != null)
            {
                List<string> mods = Utils.GetModsList(score.Mods);
                var pp = new PpCalculator(score.Beatmap.BeatmapId);
            
                score.Beatmap.PP = pp.GetFcPp(score, mods, _mode).Pp;
                score.PP = pp.GetScorePp(score, mods, _mode).Pp;

                ImageGenerator imageGenerator = _scoreType switch
                {
                    ScoreType.Best => new BestScoreImage(user, score),
                    ScoreType.Recent => new RecentScoreImage(user, score),
                    _ => null
                };
                try
                {
                    image = imageGenerator?.Generate();
                }
                catch (Exception e)
                {
                    image = Image.LoadAsync("./Static/usernotfound.png").Result;
                }
            }
            else
            {
                image = Image.LoadAsync("./Static/usernotfound.png").Result;
            }

           
           
            using var ms = new MemoryStream();
            image.SaveAsPng(ms);
            byte[] imageArray = ms.GetBuffer();
            
            return imageArray;
        }
    }
}