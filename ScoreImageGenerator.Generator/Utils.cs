using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ScoreImageGenerator.Generator.Extensions;
using ScoreImageGenerator.Generator.Objects;

namespace ScoreImageGenerator.Generator
{
    public static class Utils
    {
        public static string[] ModOrder =
        {
            "EZ", "HD", "HT", "DT", "NC", "HR", "FL", "NF",
            "SD", "PF", "RX", "AP", "SO", "AT", "V2", "TD"
        };

        public static async Task<byte[]> GetBeatmapBackground(int beatmapSetId)
        {
            var url = $"https://assets.ppy.sh/beatmaps/{beatmapSetId}/covers/cover.jpg";
            var request =  (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "image/jpeg";
            var response = await request.GetResponseAsync();
            using (Image image = await Image.LoadAsync(response.GetResponseStream()))
            {
                using (Image bg = image.Clone(x => x.ConvertToRounded(new Size(615, 170), 10)))
                {
                    await using var ms = new MemoryStream();
                    await bg.SaveAsPngAsync(ms);
                    return ms.GetBuffer();
                }
            }
        }

        public static async Task<byte[]> GetUserAvatar(string userId)
        {
            WebResponse response;
            string url = $"https://a.ppy.sh/{userId}";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "image/png";
            try
            {
                response = await request.GetResponseAsync();
            }
            catch (WebException e)
            {
                url = $"https://a.ppy.sh/";
                request = (HttpWebRequest)WebRequest.Create(url);
                response = await request.GetResponseAsync();
            }
            
            using (Image image = await Image.LoadAsync(response.GetResponseStream()))
            {
                using (Image avatar = image.Clone(x => x.ConvertToRounded(new Size(170, 170), 10)))
                {
                    await using var ms = new MemoryStream();
                    await avatar.SaveAsPngAsync(ms);
                    return ms.GetBuffer();
                }
            }
        }

        public static List<string> GetModsList(int mods)
        {
            List<string> modsList = new List<string>();
            foreach (int mod in Enum.GetValues(typeof(Mods)))
            {
                if ((mod & mods) == mod)
                    modsList.Add(Enum.GetName(typeof(Mods), mod));
            }
            
            return OrderMods(modsList);
        }

        public static List<string> OrderMods(List<string> mods)
        {
            List<string> orderedMods = new List<string>();

            foreach (string mod in ModOrder)
            {
                if (mods.Contains(mod))
                    orderedMods.Add(mod);
            }

            if (orderedMods.Contains("NC"))
                orderedMods.Remove("DT");

            if (orderedMods.Contains("PF"))
                orderedMods.Remove("SD");

            return orderedMods;
        }

        public static Color GetRankColor(string rank)
        {
            Color color = rank switch
            {
                "XH" => Color.LightGrey,
                "X" => Color.Gold,
                "SH" => Color.Silver,
                "S" => Color.Yellow,
                "A" => Color.SpringGreen,
                "B" => Color.LightSkyBlue,
                "C" => Color.Orchid,
                "D" => Color.Tomato,
                "F" => Color.DarkRed,
            };

            return color;
        }
        
    }
}