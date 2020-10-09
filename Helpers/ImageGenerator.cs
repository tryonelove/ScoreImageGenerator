using System;
using ScoreImageGenerator.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ScoreImageGenerator.Helpers
{
    public class ImageGenerator
    {
        Image<Rgba32> _image = new Image<Rgba32>(956, 454);
        User _user;
        ScoreType _scoreType;
        int _limit;
        Score _score;
        public ImageGenerator(User user, Score score, ScoreType scoreType)
        {
            _user = user;
            _score = score;
            _scoreType = scoreType;
        }
        void CreateTemplate()
        {
            const int radius = 15;
            Rgba32 color = new Rgba32(28,28,28,255);
            Utils.DrawPie(_image, color, new Point(15, 15), radius, 360);
            Utils.DrawCircle(_image, color, new Point(_image.Width-16, 15), radius);
            Utils.DrawCircle(_image, color, new Point(15, _image.Height-16), radius);
            Utils.DrawCircle(_image, color, new Point(_image.Width - 16, _image.Height-16), radius);
            Utils.DrawRecrangle(_image, color, new Point(radius, 0), _image.Width-radius*2, _image.Height);
            Utils.DrawRecrangle(_image, color, new Point(0, radius), _image.Width, _image.Height-radius*2);
        }

        void DrawBeatmapStats()
        {
            Utils.DrawRecrangle(_image, new Rgba32(98,98,98, 255), new Point(293, 39), 615, 170);
            DrawBeatmapTitle();
        }

        void DrawUserStats()
        {
            Utils.DrawRecrangle(_image, new Rgba32(98,98,98, 255), new Point(53, 39), 170, 170);
        }

        void DrawBeatmapTitle()
        {
            Utils.DrawText(_image, $"{_score.Beatmap.Title} [{_score.Beatmap.DiffName}]", 15, new Point(313, 60));
            Utils.DrawText(_image, $"mapped by {_score.Beatmap.Creator}", 15, new Point(313, 101));
        }

        public void Generate()
        {
            CreateTemplate();
            DrawBeatmapStats();
            DrawUserStats();
            _image.SaveAsPng("template.png");
            _image.Dispose();
            return;
        }
    }
}