namespace ScoreImageGenerator.Generator.Objects
{
    public static class Roboto
    {
        private const string FontFamily = "Roboto-{0}.ttf";

        public static string Black => string.Format(FontFamily, "Black");

        public static string BlackItalic => string.Format(FontFamily, "BlackItalic");

        public static string Bold => string.Format(FontFamily, "Bold");

        public static string BoldItalic => string.Format(FontFamily, "BoldItalic");

        public static string Italic => string.Format(FontFamily, "Italic");

        public static string Light => string.Format(FontFamily, "Light");

        public static string LightItalic => string.Format(FontFamily, "LightItalic");

        public static string Medium => string.Format(FontFamily, "Medium");

        public static string MediumItalic => string.Format(FontFamily, "MediumItalic");

        public static string Regular => string.Format(FontFamily, "Regular");

        public static string Thin => string.Format(FontFamily, "Thin");

        public static string ThinItalic => string.Format(FontFamily, "ThinItalic");
    }
}