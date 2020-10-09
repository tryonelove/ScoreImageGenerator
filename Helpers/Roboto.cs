namespace ScoreImageGenerator.Helpers
{
    public class Roboto
    {
        public static string FontFamily = "Roboto-{0}.ttf";
        public static string Black { get { return string.Format(FontFamily, "Black");}}
        public static string BlackItalic { get { return string.Format(FontFamily, "BlackItalic");}}
        public static string Bold { get { return string.Format(FontFamily, "Bold");}}
        public static string BoldItalic { get { return string.Format(FontFamily, "BoldItalic"); }}
        public static string Italic { get { return string.Format(FontFamily, "Italic"); }}
        public static string Light { get { return string.Format(FontFamily, "Light"); }}
        public static string LightItalic { get { return string.Format(FontFamily, "LightItalic"); }}
        public static string Medium { get { return string.Format(FontFamily, "Medium"); }}
        public static string MediumItalic { get { return string.Format(FontFamily, "MediumItalic"); }}
        public static string Regular { get { return string.Format(FontFamily, "Regular"); }}
        public static string Thin { get { return string.Format(FontFamily, "Thin"); }}
        public static string ThinItalic { get { return string.Format(FontFamily, "ThinItalic"); }}
    }
}