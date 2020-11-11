using System.Text.Json.Serialization;

namespace ScoreImageGenerator.Objects
{
    public class CalculatorResponse
    {
        [JsonPropertyName("Beatmap")]
        public string Beatmap { get; set; } 

        [JsonPropertyName("Accuracy")]
        public double Accuracy { get; set; } 

        [JsonPropertyName("Combo")]
        public double Combo { get; set; } 

        [JsonPropertyName("Great")]
        public double Great { get; set; } 

        [JsonPropertyName("Good")]
        public double Good { get; set; } 

        [JsonPropertyName("Meh")]
        public double Meh { get; set; } 

        [JsonPropertyName("Miss")]
        public double Miss { get; set; } 

        [JsonPropertyName("Mods")]
        public string Mods { get; set; } 

        [JsonPropertyName("Aim")]
        public double Aim { get; set; } 

        [JsonPropertyName("Speed")]
        public double Speed { get; set; } 

        [JsonPropertyName("OD")]
        public double OD { get; set; } 

        [JsonPropertyName("AR")]
        public double AR { get; set; } 

        [JsonPropertyName("MaxCombo")]
        public double MaxCombo { get; set; } 

        [JsonPropertyName("pp")]
        public double Pp { get; set; } 
    }
}