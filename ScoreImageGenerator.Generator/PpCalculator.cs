using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ScoreImageGenerator.Generator.Objects;

namespace ScoreImageGenerator.Generator
{
    public class PpCalculator
    {
        private readonly string _workingDirectory = Environment.CurrentDirectory + "/PerformanceCalculator";
        private const string BaseUrl = "https://osu.ppy.sh";
        private readonly int _beatmapId;
        private readonly HttpClient _client = new HttpClient();

        public PpCalculator(int beatmapId)
        {
            _beatmapId = beatmapId;
        }

        public async Task CacheBeatmap()
        {
            string beatmapPath = $"{_workingDirectory}/cache/{_beatmapId}.osu";
            if (File.Exists(beatmapPath))
            {
                return;
            }
            
            var uri = new Uri ($"{BaseUrl}/osu/{_beatmapId}");
            var osuFileStream = await _client.GetStreamAsync(uri);
            await using var fs = new FileStream(beatmapPath, FileMode.CreateNew);
            await osuFileStream.CopyToAsync(fs);
        }

        private static string GetModsArgs(IEnumerable<string> mods)
        {
            string args = string.Empty;
            foreach (string mod in mods)
            {
                args += $"-m {mod} ";
            }

            return args;
        }

        private ProcessStartInfo GetProcessStartInfo(Score score, Mode osuMode)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                WorkingDirectory = _workingDirectory,
                Arguments =
                    $"PerformanceCalculator.dll simulate {osuMode} {_workingDirectory}/cache/{_beatmapId}.osu -j "
            };

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
            
            return startInfo;
        }   
        
        public async Task<CalculatorResponse> GetFcPp(Score score, List<string> mods, Mode osuMode)
        {
            mods.Remove("NM");
            
            var scoreCopy = (Score)score.Clone();
            scoreCopy.Count300 += scoreCopy.CountMiss;
            scoreCopy.CountMiss = 0;
            
            var startInfo = GetProcessStartInfo(scoreCopy, osuMode);
            startInfo.Arguments += GetModsArgs(mods);

            var process = Process.Start(startInfo);

            return await JsonSerializer.DeserializeAsync<CalculatorResponse>(process?.StandardOutput.BaseStream);
        }

        public async Task<CalculatorResponse> GetScorePp(Score score, List<string> mods, Mode osuMode)
        {
            mods.Remove("NM");
            var startInfo = GetProcessStartInfo(score, osuMode);
            startInfo.Arguments += GetModsArgs(mods);
            
            Process process = Process.Start(startInfo);
            
            return await JsonSerializer.DeserializeAsync<CalculatorResponse>(process?.StandardOutput.BaseStream);
        }
    }
}