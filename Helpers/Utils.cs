using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Drawing.Processing;

namespace ScoreImageGenerator.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// Method <c>DrawCircle</c> renders a circle
        /// with a center in <c>x</c> and <c>y</c>, 
        /// with specified <c>radius</c>  and <c>color</c>
        ///</summary>
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
            for(int y=start.Y; y<start.Y+height; y++)
            {
                for(int x=start.X; x<start.X+width; x++)
                {
                    image[x, y] = color;
                }
            }
        }
        public static void DrawRoundedRecrangle(Image<Rgba32> image, int radius, Rgba32 color, int x0, int y0, int x1, int y1)
        {

        }

        /// <summary>
        /// Draw a pie on the <c>image</c>
        /// </summary>
        /// <param name="image">Image<Rgba32> image</param>
        /// <param name="color">RGBA color</param>
        /// <param name="center">X and Y center coordinates</param>
        public static void DrawPie(Image<Rgba32> image, Rgba32 color, Point center, int radius, int angle)
        {
            int x, y;
            for (int r = radius; r > 0; r--)
            {
                for (int _angle = 0; _angle < angle; _angle++)
                {
                    x = Convert.ToInt32(Math.Cos(_angle) * r) + center.X;
                    y = Convert.ToInt32(Math.Sin(_angle) * r) + center.Y;
                    image[x, y] = color;
                }
            }
        }

        public static void DrawText(Image<Rgba32> image, string text, int fontSize, Point coords)
        {
            FontCollection collection = new FontCollection();
            FontFamily family = collection.Install("/home/tryonelove/Documents/ScoreImageGenerator/Fonts/Roboto-Medium.ttf");
            Font font = family.CreateFont(fontSize);
            // idk method is not available
            image.Mutate(x => x.DrawText(text, font, Color.White, coords));
        }
    }
}