using System;
using System.Windows.Media;

namespace DevGearbox.Utils;

public static class ColorFormatConverter
{
    public static string ToHex(Color color)
    {
        return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static string ToHexAlpha(Color color)
    {
        return $"#{color.A:X2}{color.R:X2}{color.G:X2}{color.B:X2}";
    }

    public static string ToRgb(Color color)
    {
        return $"rgb({color.R}, {color.G}, {color.B})";
    }

    public static string ToRgba(Color color)
    {
        double alpha = Math.Round(color.A / 255.0, 2);
        return $"rgba({color.R}, {color.G}, {color.B}, {alpha})";
    }

    public static string ToHsl(Color color)
    {
        var (h, s, l) = RgbToHsl(color.R, color.G, color.B);
        return $"hsl({h:F0}°, {s:F0}%, {l:F0}%)";
    }

    public static string ToHsla(Color color)
    {
        var (h, s, l) = RgbToHsl(color.R, color.G, color.B);
        double alpha = Math.Round(color.A / 255.0, 2);
        return $"hsla({h:F0}°, {s:F0}%, {l:F0}%, {alpha})";
    }

    public static string ToHsb(Color color)
    {
        var (h, s, b) = RgbToHsb(color.R, color.G, color.B);
        return $"hsb({h:F0}°, {s:F0}%, {b:F0}%)";
    }

    public static string ToHwb(Color color)
    {
        var (h, w, b) = RgbToHwb(color.R, color.G, color.B);
        return $"hwb({h:F0}°, {w:F0}%, {b:F0}%)";
    }

    public static string ToCmyk(Color color)
    {
        var (c, m, y, k) = RgbToCmyk(color.R, color.G, color.B);
        return $"cmyk({c:F0}%, {m:F0}%, {y:F0}%, {k:F0}%)";
    }

    public static Color FromHex(string hex)
    {
        hex = hex.TrimStart('#');
        
        if (hex.Length == 6)
        {
            byte r = Convert.ToByte(hex.Substring(0, 2), 16);
            byte g = Convert.ToByte(hex.Substring(2, 2), 16);
            byte b = Convert.ToByte(hex.Substring(4, 2), 16);
            return Color.FromRgb(r, g, b);
        }
        else if (hex.Length == 8)
        {
            byte a = Convert.ToByte(hex.Substring(0, 2), 16);
            byte r = Convert.ToByte(hex.Substring(2, 2), 16);
            byte g = Convert.ToByte(hex.Substring(4, 2), 16);
            byte b = Convert.ToByte(hex.Substring(6, 2), 16);
            return Color.FromArgb(a, r, g, b);
        }
        else if (hex.Length == 3)
        {
            byte r = Convert.ToByte($"{hex[0]}{hex[0]}", 16);
            byte g = Convert.ToByte($"{hex[1]}{hex[1]}", 16);
            byte b = Convert.ToByte($"{hex[2]}{hex[2]}", 16);
            return Color.FromRgb(r, g, b);
        }

        throw new ArgumentException("Invalid HEX color format");
    }

    private static (double h, double s, double l) RgbToHsl(byte r, byte g, byte b)
    {
        double rd = r / 255.0;
        double gd = g / 255.0;
        double bd = b / 255.0;

        double max = Math.Max(rd, Math.Max(gd, bd));
        double min = Math.Min(rd, Math.Min(gd, bd));
        double delta = max - min;

        double h = 0, s = 0, l = (max + min) / 2;

        if (delta != 0)
        {
            s = l > 0.5 ? delta / (2 - max - min) : delta / (max + min);

            if (max == rd)
                h = ((gd - bd) / delta) + (gd < bd ? 6 : 0);
            else if (max == gd)
                h = ((bd - rd) / delta) + 2;
            else
                h = ((rd - gd) / delta) + 4;

            h /= 6;
        }

        return (h * 360, s * 100, l * 100);
    }

    private static (double h, double s, double b) RgbToHsb(byte r, byte g, byte b)
    {
        double rd = r / 255.0;
        double gd = g / 255.0;
        double bd = b / 255.0;

        double max = Math.Max(rd, Math.Max(gd, bd));
        double min = Math.Min(rd, Math.Min(gd, bd));
        double delta = max - min;

        double h = 0, s = max == 0 ? 0 : delta / max, v = max;

        if (delta != 0)
        {
            if (max == rd)
                h = ((gd - bd) / delta) + (gd < bd ? 6 : 0);
            else if (max == gd)
                h = ((bd - rd) / delta) + 2;
            else
                h = ((rd - gd) / delta) + 4;

            h /= 6;
        }

        return (h * 360, s * 100, v * 100);
    }

    private static (double h, double w, double b) RgbToHwb(byte r, byte g, byte b)
    {
        var (h, s, v) = RgbToHsb(r, g, b);
        double w = (1 - s / 100) * v;
        double bl = 100 - v;
        return (h, w, bl);
    }

    private static (double c, double m, double y, double k) RgbToCmyk(byte r, byte g, byte b)
    {
        if (r == 0 && g == 0 && b == 0)
            return (0, 0, 0, 100);

        double rd = r / 255.0;
        double gd = g / 255.0;
        double bd = b / 255.0;

        double k = 1 - Math.Max(rd, Math.Max(gd, bd));
        double c = (1 - rd - k) / (1 - k);
        double m = (1 - gd - k) / (1 - k);
        double y = (1 - bd - k) / (1 - k);

        return (c * 100, m * 100, y * 100, k * 100);
    }
}

