using System;
using ScoreImageGenerator.Helpers;
using SixLabors.Fonts;
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

        void DrawBeatmapStats(FontFamily family)
        {
            Font font = family.CreateFont(size: 15);
            Utils.DrawRecrangle(_image, new Rgba32(98,98,98, 255), new Point(293, 39), 615, 170);
            DrawBeatmapTitle(font);
        }

        void DrawUserStats(FontFamily family)
        {
            Font font = family.CreateFont(size: 15);
            Utils.DrawRecrangle(_image, new Rgba32(98,98,98, 255), new Point(53, 39), 170, 170);
            Utils.DrawText(_image, $"#{_user.Rank}", font, Color.White, new Point(62, 150));
            Utils.DrawText(_image, _user.Username, font, Color.White, new Point(62, 167));
            Utils.DrawText(_image, $"{_user.PP}pp", font, Color.White, new Point(62, 185));
        }

        void DrawBeatmapTitle(Font font)
        {
            Utils.DrawText(_image, $"{_score.Beatmap.Title} [{_score.Beatmap.DiffName}]", font, Color.White, new Point(313, 60));
            Utils.DrawText(_image, $"mapped by {_score.Beatmap.Creator}", font, Color.White, new Point(313, 101));
            Utils.DrawText(_image, $"CS: {_score.Beatmap.CS}", font, Color.White, new Point(315, 185));
            Utils.DrawText(_image, $"AR: {_score.Beatmap.AR}", font, Color.White, new Point(385, 185));
            Utils.DrawText(_image, $"OD: {_score.Beatmap.OD}", font, Color.White, new Point(455, 185));
            Utils.DrawText(_image, $"HP: {_score.Beatmap.HP}", font, Color.White, new Point(525, 185));
            Utils.DrawText(_image, $"BPM: {_score.Beatmap.BPM}", font, Color.White, new Point(595, 185));
            Utils.DrawText(_image, $"Stars: {_score.Beatmap.Stars}", font, Color.White, new Point(691, 185));
            Utils.DrawText(_image, $"Length: {_score.Beatmap.Length}", font, Color.White, new Point(787, 185));
        }

        void DrawScoreStats(FontFamily family)
        {
            Font font = family.CreateFont(size: 21);
            Rgba32 color = new Rgba32(185,185,185,255);
            // Draw middle image info about score
            Utils.DrawText(_image, "Score", font, color, new Point(48, 245));
            Utils.DrawText(_image, $"{_score.ScoreValue}", font, Color.White, new Point(48, 284));

            Utils.DrawText(_image, "Rank", font, color, new Point(343, 245));
            Utils.DrawText(_image, $"{_score.Rank}", font, Color.Yellow, new Point(357, 284));

            Utils.DrawText(_image, "Combo", font, color, new Point(538, 245));
            Utils.DrawText(_image, $"{_score.Combo}/{_score.Beatmap.MaxCombo}", font, color, new Point(538, 284));

            Utils.DrawText(_image, "Mods", font, color, new Point(817, 245));
            Utils.DrawText(_image, $"{_score.Mods}", font, color, new Point(817, 284));

            // Draw hit circles accuracy
            Utils.DrawText(_image, "300", font, color, new Point(48, 350));
            Utils.DrawText(_image, $"{_score.Count300}", font, Color.White, new Point(48, 382));

            Utils.DrawText(_image, "100", font, color, new Point(138, 350));
            Utils.DrawText(_image, $"{_score.Count100}", font, Color.White, new Point(138, 382));

            Utils.DrawText(_image, "50", font, color, new Point(218, 350));
            Utils.DrawText(_image, $"{_score.Count50}", font, Color.White, new Point(218, 382));

            Utils.DrawText(_image, "X", font, color, new Point(280, 350));
            Utils.DrawText(_image, $"{_score.CountMiss}", font, Color.White, new Point(280, 382));

            Utils.DrawText(_image, "Accuracy", font, color, new Point(374, 350));
            Utils.DrawText(_image, $"{_score.Accuracy}%", font, Color.White, new Point(374, 382));

            Utils.DrawText(_image, "Completion", font, color, new Point(517, 350));
            Utils.DrawText(_image, $"{_score.Accuracy}%", font, Color.White, new Point(517, 382));
            Utils.DrawText(_image, "Performance", font, color, new Point(690, 350));
            Utils.DrawText(_image, $"{_score.PP}pp", font, Color.White, new Point(690, 382));
            Utils.DrawText(_image, "If FC", font, color, new Point(836, 350));
            Utils.DrawText(_image, $"{_score.Beatmap.PP}", font, Color.White, new Point(836, 382));
        }

        public void Generate()
        {
            FontCollection collection = new FontCollection();
            FontFamily family = collection.Install($"./Fonts/{Roboto.Medium}");
            // Rendering background and templates for score stats
            CreateTemplate();
            // Drawing data on the image
            DrawBeatmapStats(family);
            DrawUserStats(family);
            DrawScoreStats(family);
            _image.SaveAsPng("template.png");
            _image.Dispose();
        }
    }
}