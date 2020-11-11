using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ScoreImageGenerator.Objects;

namespace ScoreImageGenerator.Helpers
{
    public class PpCalculator
    {
        private string _workingDirectory = Environment.CurrentDirectory + "/PerformanceCalculator";
        private const string baseUrl = "https://osu.ppy.sh";
        private int _beatmapId;

        public int BeatmapId { get => _beatmapId; set => _beatmapId = value; }

        public PpCalculator(int beatmapId)
        {
            _beatmapId = beatmapId;
        }

        private void CacheBeatmap()
        {
            string beatmapPath = $"{_workingDirectory}/cache/{_beatmapId}.osu";
            if (File.Exists(beatmapPath))
            {
                return;
            }
            
            Uri uri = new Uri ($"{baseUrl}/osu/{_beatmapId}");
            // Create a FileWebRequest object.
            using (var client = new WebClient())
            {
                Task download = client.DownloadFileTaskAsync(uri, beatmapPath);
                Task.WaitAll(download);
            }
        }

        private string GetModsArgs(List<string> mods)
        {
            string args = string.Empty;
            foreach (var mod in mods)
            {
                args += $"-m {mod} ";
            }

            return args;
        }

        public CalculatorResponse GetFcPp(List<string> mods, OsuMode osuMode)
        {
            CacheBeatmap();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "dotnet";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WorkingDirectory = _workingDirectory;
            startInfo.Arguments = $"PerformanceCalculator.dll simulate {osuMode} {_workingDirectory}/cache/{_beatmapId}.osu -j ";
            startInfo.Arguments += GetModsArgs(mods);
            
            Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();

            return JsonSerializer.Deserialize<CalculatorResponse>(output);
        }

        public CalculatorResponse GetScorePp(Score score, List<string> mods, OsuMode osuMode)
        {
            CacheBeatmap();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "dotnet";
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.WorkingDirectory = _workingDirectory;
            startInfo.Arguments = $"PerformanceCalculator.dll simulate {osuMode} {_workingDirectory}/cache/{_beatmapId}.osu -j ";
            startInfo.Arguments += GetModsArgs(mods);

            if (string.Compare(osuMode.ToString(), "mania", StringComparison.CurrentCulture) == 0)
            {   
                startInfo.Arguments += $"-s {score.ScoreValue} ";
            }
            else
            {
                startInfo.Arguments += $"-a {score.Accuracy} ";
                startInfo.Arguments += $"-c {score.Combo} ";
                startInfo.Arguments += $"-X {score.CountMiss} ";
            }

            Process process = Process.Start(startInfo);
            string output = process.StandardOutput.ReadToEnd();

            return JsonSerializer.Deserialize<CalculatorResponse>(output);
        }
    }
}