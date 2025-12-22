using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevGearbox.Components;

/// <summary>
/// Interaction logic for ColorConverterView.xaml
/// </summary>
public partial class ColorConverterView : UserControl
{
    private Color _currentColor = Colors.Black;

    public ColorConverterView()
    {
        InitializeComponent();
        UpdateColorOutputs(Colors.Black);
    }

    private void HexInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(HexInput.Text))
                return;

            var color = Utils.ColorFormatConverter.FromHex(HexInput.Text);
            _currentColor = color;
            UpdateColorPreview(color);
            UpdateColorOutputs(color);
        }
        catch
        {
            // Invalid hex, ignore
        }
    }

    private void RedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateFromRgb();
    }

    private void GreenSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateFromRgb();
    }

    private void BlueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateFromRgb();
    }

    private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        UpdateFromRgb();
    }

    private void UpdateFromRgb()
    {
        if (RedSlider == null || GreenSlider == null || BlueSlider == null || AlphaSlider == null)
            return;

        byte r = (byte)RedSlider.Value;
        byte g = (byte)GreenSlider.Value;
        byte b = (byte)BlueSlider.Value;
        byte a = (byte)AlphaSlider.Value;

        var color = Color.FromArgb(a, r, g, b);
        _currentColor = color;

        // Update hex input without triggering TextChanged
        HexInput.TextChanged -= HexInput_TextChanged;
        HexInput.Text = Utils.ColorFormatConverter.ToHex(color);
        HexInput.TextChanged += HexInput_TextChanged;

        UpdateColorPreview(color);
        UpdateColorOutputs(color);
    }

    private void UpdateColorPreview(Color color)
    {
        if (ColorPreview != null)
        {
            ColorPreview.Fill = new SolidColorBrush(color);
        }
    }

    private void UpdateColorOutputs(Color color)
    {
        if (HexOutput == null) return;

        HexOutput.Text = Utils.ColorFormatConverter.ToHex(color);
        HexAlphaOutput.Text = Utils.ColorFormatConverter.ToHexAlpha(color);
        RgbOutput.Text = Utils.ColorFormatConverter.ToRgb(color);
        RgbaOutput.Text = Utils.ColorFormatConverter.ToRgba(color);
        HslOutput.Text = Utils.ColorFormatConverter.ToHsl(color);
        HslaOutput.Text = Utils.ColorFormatConverter.ToHsla(color);
        HsbOutput.Text = Utils.ColorFormatConverter.ToHsb(color);
        HwbOutput.Text = Utils.ColorFormatConverter.ToHwb(color);
        CmykOutput.Text = Utils.ColorFormatConverter.ToCmyk(color);

        // Update RGB value labels
        RedValue.Text = color.R.ToString();
        GreenValue.Text = color.G.ToString();
        BlueValue.Text = color.B.ToString();
        AlphaValue.Text = color.A.ToString();
    }

    private void PresetColor_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string colorName)
        {
            Color color = colorName switch
            {
                "Red" => Colors.Red,
                "Green" => Colors.Green,
                "Blue" => Colors.Blue,
                "Yellow" => Colors.Yellow,
                "Cyan" => Colors.Cyan,
                "Magenta" => Colors.Magenta,
                "White" => Colors.White,
                "Black" => Colors.Black,
                "Gray" => Colors.Gray,
                "Orange" => Colors.Orange,
                "Purple" => Colors.Purple,
                "Pink" => Colors.Pink,
                _ => Colors.Black
            };

            SetColor(color);
        }
    }

    private void SetColor(Color color)
    {
        _currentColor = color;

        // Update sliders
        RedSlider.Value = color.R;
        GreenSlider.Value = color.G;
        BlueSlider.Value = color.B;
        AlphaSlider.Value = color.A;

        // Update hex input
        HexInput.TextChanged -= HexInput_TextChanged;
        HexInput.Text = Utils.ColorFormatConverter.ToHex(color);
        HexInput.TextChanged += HexInput_TextChanged;

        UpdateColorPreview(color);
        UpdateColorOutputs(color);
    }

    private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is string format)
        {
            string value = format switch
            {
                "Hex" => HexOutput.Text,
                "HexAlpha" => HexAlphaOutput.Text,
                "RGB" => RgbOutput.Text,
                "RGBA" => RgbaOutput.Text,
                "HSL" => HslOutput.Text,
                "HSLA" => HslaOutput.Text,
                "HSB" => HsbOutput.Text,
                "HWB" => HwbOutput.Text,
                "CMYK" => CmykOutput.Text,
                _ => ""
            };

            if (!string.IsNullOrEmpty(value))
            {
                Clipboard.SetText(value);
            }
        }
    }
}

