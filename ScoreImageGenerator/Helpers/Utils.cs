using System;
using System.IO;
using System.Net;
using System.Text;
using ScoreImageGenerator.Extensions;
using ScoreImageGenerator.Objects;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ScoreImageGenerator.Helpers
{
    public static class Utils
    {
        public static byte[] GetBeatmapBackground(int beatmapSetId)
        {
            var url = $"https://assets.ppy.sh/beatmaps/{beatmapSetId}/covers/cover.jpg";
            var request =  (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "image/jpeg";
            var response = request.GetResponseAsync().Result;
            using (Image image = Image.LoadAsync(response.GetResponseStream()).Result)
            {
                using (Image bg = image.Clone(x => x.ConvertToRounded(new Size(615, 170), 10)))
                {
                    using var ms = new MemoryStream();
                    bg.SaveAsPng(ms);
                    return ms.GetBuffer();
                }
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
                using (Image avatar = image.Clone(x => x.ConvertToRounded(new Size(170, 170), 10)))
                {
                    using var ms = new MemoryStream();
                    avatar.SaveAsPng(ms);
                    return ms.GetBuffer();
                }
            }
        }

        public static string GetStringModsRepr(int mods)
        {
            string strMods = string.Empty;
            foreach (int mod in Enum.GetValues(typeof(Mods)))
            {
                if ((mod & mods) == mod)
                {
                    strMods += Enum.GetName(typeof(Mods), mod);
                }
            }

            if (strMods.Length > 2)
            {
                return strMods.Replace("NM", "");
            }

            if (strMods.Contains("NC") && strMods.Contains("DT"))
            {
                return strMods.Replace("NC", "");
            }
            
            return strMods;
        }
    }
}