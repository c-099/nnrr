using System.Drawing;

namespace nnrr
{
    public static class Theme
    {
        public static Color Primary = Color.FromArgb(17, 17, 17);      // Deep Background
        public static Color Secondary = Color.FromArgb(45, 45, 45);    // Surface/Card (Brighter)
        public static Color Accent = Color.FromArgb(255, 71, 87);      // Crimson Red
        public static Color Text = Color.FromArgb(255, 255, 255);      // White
        public static Color TextDim = Color.FromArgb(160, 160, 160);   // Dimmed Text
        
        public static Color ButtonHover = Color.FromArgb(60, 60, 60);
        public static Color ButtonDown = Color.FromArgb(75, 75, 75);

        public static Font FontRegular = new Font("Segoe UI", 9F, FontStyle.Regular);
        public static Font FontLarge = new Font("Segoe UI", 11F, FontStyle.Regular);
        public static Font FontSmall = new Font("Segoe UI", 8F, FontStyle.Regular);
    }
}
