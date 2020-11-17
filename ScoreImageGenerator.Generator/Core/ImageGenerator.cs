using System;
using System.Collections.Generic;
using System.Globalization;
using ScoreImageGenerator.Generator.Objects;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ScoreImageGenerator.Generator.Core
{
    public class ImageGenerator
    {
        private readonly Image<Rgba32> _image = new Image<Rgba32>(956, 454);
        private readonly User _user;
        private readonly Score _score;
        delegate void Drawer(FontFamily family);
        
        protected ImageGenerator(User user, Score score)
        {
            _user = user;
            _score = score;
        }
        
        private void CreateTemplate()
        {
            var color = new Rgba32(28,28,28,255);
            _image.Mutate(x => x.Fill(color));
        }

        private void DrawBeatmapStats(FontFamily family)
        {
            var font = family.CreateFont(15);
            _image.Mutate(x => x
                .DrawImage(_image, new Point(293, 39), 0.3f)
            );
            DrawBeatmapTitle(font);
        }

        private void DrawUserStats(FontFamily family)
        {
            var font = family.CreateFont(15);
            var avatar = Image.Load(_user.Avatar);
            
            // Paste avatar
            _image.Mutate(x => x
                .DrawImage(avatar, new Point(48, 39), 0.3f)
                // Draw user stats
                .DrawText($"#{_user.Rank}", font, Color.White, new Point(62, 150))
                .DrawText(_user.Username, font, Color.White, new Point(62, 167))
                .DrawText($"{Math.Round(_user.PP)}pp", font, Color.White, new Point(62, 185)));
        }

        private void DrawBeatmapTitle(Font font)
        {
            var background = Image.Load(_score.Beatmap.BackgroundImage);

            // Paste background
            _image.Mutate(x => x
                .DrawImage(background, new Point(295, 40), 0.3f)
            );

            // Draw beatmap stats
            _image.Mutate(x => x
                .DrawText($"{_score.Beatmap.Title} [{_score.Beatmap.DiffName}]", font, Color.White, new Point(315, 61))
                .DrawText($"mapped by {_score.Beatmap.Creator}", font, Color.White, new Point(315, 102))
                .DrawText($"CS: {Math.Round(_score.Beatmap.CS, 2)}", font, Color.White, new Point(315, 185))
                .DrawText($"AR: {Math.Round(_score.Beatmap.AR, 2)}", font, Color.White, new Point(385, 185))
                .DrawText($"OD: {Math.Round(_score.Beatmap.OD, 2)}", font, Color.White, new Point(455, 185))
                .DrawText($"HP: {Math.Round(_score.Beatmap.HP, 2)}", font, Color.White, new Point(525, 185))
                .DrawText($"BPM: {Math.Round(_score.Beatmap.BPM, 2)}", font, Color.White, new Point(595, 185))
                .DrawText($"Stars: {Math.Round(_score.Beatmap.Stars, 2)}", font, Color.White, new Point(691, 185))
                .DrawText($"Length: {_score.Beatmap.Length}", font, Color.White, new Point(787, 185)));
        }

        private void DrawScoreStats(FontFamily family)
        {
            List<string> mods = Utils.GetModsList(_score.Mods);
            if (mods.Count == 0)
            {
                mods.Add("NM");
            }
            
            var font = family.CreateFont(size: 21);
            var color = new Rgba32(185,185,185,255);
            // Draw middle image info about score
            _image.Mutate(x => x
                .DrawText("Score", font, color, new Point(48, 245))
                .DrawText($"{_score.ScoreValue.ToString("#,#", CultureInfo.CurrentCulture)}", font, Color.White, new Point(48, 284))
                .DrawText("Rank", font, color, new Point(343, 245))
                .DrawText($"{_score.Rank}", font, Color.Yellow, new Point(357, 284))
                .DrawText("Combo", font, color, new Point(538, 245))
                .DrawText($"{_score.Combo}/{_score.Beatmap.MaxCombo}", font, Color.White, new Point(538, 284))
                .DrawText("Mods", font, color, new Point(817, 245))
                .DrawText($"{string.Join("", mods)}", font, Color.White, new Point(817, 284))

                // Draw hit circles accuracy
                .DrawText("300", font, color, new Point(48, 350))
                .DrawText($"{_score.Count300}", font, Color.Aqua, new Point(48, 382))
                .DrawText("100", font, color, new Point(138, 350))
                .DrawText($"{_score.Count100}", font, Color.Green, new Point(138, 382))
                .DrawText("50", font, color, new Point(218, 350))
                .DrawText($"{_score.Count50}", font, Color.DodgerBlue, new Point(218, 382))
                .DrawText("X", font, color, new Point(280, 350))
                .DrawText($"{_score.CountMiss}", font, Color.Maroon, new Point(280, 382))
                .DrawText("Accuracy", font, color, new Point(402, 350))
                .DrawText($"{Math.Round(_score.Accuracy, 2)}%", font, Color.White, new Point(408, 382))
                .DrawText("Performance", font, color, new Point(626, 350))
                .DrawText($"{Math.Round(_score.PP, 2)}pp", font, Color.White, new Point(645, 382))
                .DrawText("If FC", font, color, new Point(826, 350))
                .DrawText($"{Math.Round(_score.Beatmap.PP, 2)}", font, Color.White, new Point(815, 382)));
        }

        public Image Generate()
        {
            Drawer draw = null;
            var collection = new FontCollection();
            var family = collection.Install($"./Static/fonts/{Roboto.Medium}");

            // Rendering background and templates for score stats
            CreateTemplate();

            // Drawing data on the image
            draw+=DrawBeatmapStats;
            draw+=DrawUserStats;
            draw+=DrawScoreStats;            
            draw.Invoke(family);

            return _image;
        }
    }
}