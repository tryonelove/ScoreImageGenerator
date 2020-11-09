using System;
using System.IO;
using System.Net;
using ScoreImageGenerator.Extensions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Processing;

namespace ScoreImageGenerator.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Method <c>DrawCircle</c> renders a circle
        /// with a center in <c>x</c> and <c>y</c>, 
        /// with specified <c>radius</c>  and <c>color</c>
        ///</summary>
        ///<param name="image">Image to be drawn on</param>
        /// <param name="color">Rgba32 color</param>
        /// <param name="center">Center of the circle</param>
        /// <param name="radius">Circle radius</param>
        public static void DrawCircle(Image<Rgba32> image, Rgba32 color, Point center, int radius)
        {
            int x, y;
            for (int r = radius; r > 0; r--)
            {
                for (int angle = 0; angle < 360; angle++)
                {
                    x = Convert.ToInt32(Math.Cos(angle) * r) + center.X;
                    y = Convert.ToInt32(Math.Sin(angle) * r) + center.Y;
                    image[x, y] = color;
                }
            }
        }

        public static void DrawRecrangle(Image<Rgba32> image, Rgba32 color, Point start, int width, int height)
        {
            for (int y = start.Y; y < start.Y + height; y++)
            {
                for (int x = start.X; x < start.X + width; x++)
                {
                    image[x, y] = color;
                }
            }
        }

        public static Image<Rgba32> ResizeImage(Image<Rgba32> image, Size size)
        {
            image.Mutate(x => x
                .Resize(size));
            return image;
        }

        public static byte[] GetBeatmapBackground(int beatmapSetId)
        {
            var url = $"https://assets.ppy.sh/beatmaps/{beatmapSetId}/covers/cover.jpg";
            var request =  (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "image/jpeg";
            var response = request.GetResponse();
            using (Image image = Image.Load(response.GetResponseStream()))
            {
                Image bg = image.Clone(x => x.ConvertToRounded(new Size(615, 170), 10));
                using var ms = new MemoryStream();
                bg.SaveAsPng(ms);
                return ms.GetBuffer();
            }
        }

        public static byte[] GetUserAvatar(string userId)
        {
            var url = $"https://a.ppy.sh/{userId}";
            var request = (HttpWebRequest) WebRequest.Create(url);
            request.Accept = "image/png";
            var response = request.GetResponse();
            using (Image image = Image.Load(response.GetResponseStream()))
            {
                Image avatar = image.Clone(x => x.ConvertToRounded(new Size(170, 170), 10));
                using var ms = new MemoryStream(); 
                avatar.SaveAsPng(ms);
                return ms.GetBuffer();
            }
        }
    }
}