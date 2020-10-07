using System;
using ScoreImageGenerator.Helpers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ScoreImageGenerator
{
    public class ImageGenerator
    {
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
        void DrawPie()
        {
            const int radius = 10;
            using (var image = new Image<Rgba32>(956, 454))
            {
                for (int y = 0; y < radius; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                    for (int x = radius; x > 0; x--)
                    {
                        var val = Convert.ToInt32(Math.Sqrt(Math.Pow(x, 2)+Math.Pow(y,2)));
                        pixelRowSpan[val] = new Rgba32(28,28,28, 255);
                    }
                }
                image.SaveAsPng("template.png");
            }
        }
        void CreateTemplate()
        {
            const int radius = 15;
            int width = 956;
            int height = 454;
            var image = new Image<Rgba32>(width, height);
            Rgba32 color = new Rgba32(228,28,28,255);
            Utils.DrawPie(image, color, 15, 15, radius, 360);
            Utils.DrawCircle(image, color, width-16, 15, radius);
            Utils.DrawCircle(image, color, 15, height-16, radius);
            Utils.DrawCircle(image, color, width - 16, height-16, radius);
            Utils.DrawRecrangle(image, color, 15, 0, width-15, height);
            Utils.DrawRecrangle(image, color, 0, 15, width, height-15);
            image.SaveAsPng("template.png");
            image.Dispose();
        }
        public void Generate()
        {
            CreateTemplate();
            // DrawPie();
            return;
        }
    }
}