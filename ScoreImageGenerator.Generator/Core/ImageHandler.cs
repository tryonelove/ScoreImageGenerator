using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ScoreImageGenerator.Generator.API;
using ScoreImageGenerator.Generator.API.Responses;
using ScoreImageGenerator.Generator.Objects;
using SixLabors.ImageSharp;

namespace ScoreImageGenerator.Generator.Core
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

        private async Task<Score> GetBestScoreAsync(User user)
        {
            GetUserBestRequest userRequest = new GetUserBestRequest(user.Username, _mode, _limit);
            List<GetUserBestResponse> userBestResponses = await userRequest.PerformAsync();
            if (userBestResponses.Count == 0)
            {
                return null;
            }

            GetUserBestResponse resp = userBestResponses[_limit-1];

            GetBeatmapsRequest bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1);
            List<GetBeatmapsResponse> bmapResponse = await bmapRequest.PerformAsync();
            if (bmapResponse.Count == 0)
            {
                return null;
            }

            var bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = await Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };
            
            return new Score(resp, bmap) { Mode = _mode};
        }

        private async Task<Score> GetRecentScoreAsync(User user)
        {
            var request = new GetUserRecentRequest(user.Username, _mode, _limit);
            var response = await request.PerformAsync();
            if (response.Count == 0)
            {
                return null;
            }

            GetUserRecentResponse resp = response[_limit-1];
            
            var bmapRequest = new GetBeatmapsRequest(b: int.Parse(resp.BeatmapId), limit: 1);
            var bmapResponse = await bmapRequest.PerformAsync();
            if (bmapResponse.Count == 0)
            {
                return null;
            }

            var bmapResp = bmapResponse.First();
            Beatmap bmap = new Beatmap(bmapResp)
            {
                BackgroundImage = await Utils.GetBeatmapBackground(int.Parse(bmapResp.BeatmapsetId))
            };

            return new Score(resp, bmap) { Mode = _mode};
        }

        private async Task<User> GetUserAsync(string username, Mode m = Mode.Osu)
        {
            GetUserRequest request = new GetUserRequest(username, m);
            List<GetUserResponse> getUserResponseList = await request.PerformAsync();
            if (getUserResponseList.Count == 0)
            {
                return null;
            }

            var userResponse = getUserResponseList.First();
            var user = new User(userResponse)
            {
                Avatar = await Utils.GetUserAvatar(userResponse.UserId)
            };

            return user;
        }

        public async Task<Image> GetImageAsync()
        {
            Image image = await Image.LoadAsync("./Static/usernotfound.png");

            var user = await GetUserAsync(_username, _mode);
            if (user is null)
            {
                return image;
            }
            
            var score = _scoreType switch
            {
                ScoreType.Best => await GetBestScoreAsync(user),
                ScoreType.Recent => await GetRecentScoreAsync(user),
                _ => null
            };
            if (score is null)
            {
                return image;
            }
            
            List<string> mods = Utils.GetModsList(score.Mods);
            var pp = new PpCalculator(score.Beatmap.BeatmapId);
            await pp.CacheBeatmap();
            score.Beatmap.PP = (await pp.GetFcPp(score, mods, _mode)).Pp;
            score.PP = (await pp.GetScorePp(score, mods, _mode)).Pp;
            
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
                // ignored
            }

            return image;
        }

        public async Task<byte[]> GetByteImageAsync()
        {
            Image image = await GetImageAsync();
            await using var ms = new MemoryStream();
            await image.SaveAsPngAsync(ms);
            byte[] imageArray = ms.GetBuffer();
            
            return imageArray;
        }
    }
}