using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using ScoreImageGenerator.Extensions;
using ScoreImageGenerator.Objects;
using SixLabors.ImageSharp;
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

        public static List<string> GetModsList(int mods)
        {
            List<string> strMods = new List<string>();
            foreach (int mod in Enum.GetValues(typeof(Mods)))
            {
                if ((mod & mods) == mod)
                {
                    strMods.Add(Enum.GetName(typeof(Mods), mod));
                }
            }
            
            strMods.Remove("NM");

            if (strMods.Contains("NC") && strMods.Contains("DT"))
            {
                strMods.Remove("NC");
            }
            
            return strMods;
        }
        
        
    }
}