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
        void CreateTemplate()
        {
            using (var image = new Image<Rgba32>(956, 454))
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Span<Rgba32> pixelRowSpan = image.GetPixelRowSpan(y);
                    for (int x = 0; x < image.Width; x++)
                    {
                        pixelRowSpan[x] = new Rgba32(28,28,28, 255);
                    }
                }
                image.SaveAsPng("nigger.png");
            }
        }
        public void Generate()
        {
            CreateTemplate();
            return;
        }
    }
}