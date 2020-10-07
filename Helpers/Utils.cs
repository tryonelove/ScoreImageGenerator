using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace ScoreImageGenerator
{
    public static class Utils
    {
        /// <summary>
        /// Method <c>DrawCircle</c> renders a circle
        /// with a center in <c>x</c> and <c>y</c>, 
        /// with specified <c>radius</c>  and <c>color</c>
        ///</summary>
        public static void DrawCircle(Image<Rgba32> image, Rgba32 color, int x, int y, int radius)
        {
            int _x;
            int _y;
            for (int r = radius; r > 0; r--)
            {
                for (int angle = 0; angle < 360; angle++)
                {
                    _x = Convert.ToInt32(Math.Cos(angle) * r) + x;
                    _y = Convert.ToInt32(Math.Sin(angle) * r) + y;
                    image[_x, _y] = color;
                }
            }
        }

        public static void DrawRecrangle(Image<Rgba32> image, Rgba32 color, int x0, int y0, int x1, int y1)
        {
            for(int y=y0; y<y1; y++)
            {
                for(int x=x0; x<x1; x++)
                {
                    image[x, y] = color;
                }
            }
        }

        public static void DrawPie(Image<Rgba32> image, Rgba32 color, int x, int y, int radius, int angle)
        {
            int _x;
            int _y;
            for (int r = radius; r > 0; r--)
            {
                for (int _angle = 0; _angle < angle; _angle++)
                {
                    _x = Convert.ToInt32(Math.Cos(_angle) * r) + x;
                    _y = Convert.ToInt32(Math.Sin(_angle) * r) + y;
                    image[_x, _y] = color;
                }
            }
        }
    }
}