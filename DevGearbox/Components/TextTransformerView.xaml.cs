using System;
using System.Windows;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class TextTransformerView : UserControl

{
    public TextTransformerView()
    {
        InitializeComponent();
    }
    private void TextUpperCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToUpperCase);
    }

    private void TextLowerCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToLowerCase);
    }

    private void TextPascalCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToPascalCase);
    }

    private void TextCamelCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToCamelCase);
    }

    private void TextSnakeCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToSnakeCase);
    }

    private void TextKebabCase_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.ToKebabCase);
    }

    private void TextReverse_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.Reverse);
    }

    private void TextRemoveWhitespace_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.RemoveWhitespace);
    }

    private void TextUrlEncode_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.UrlEncode);
    }

    private void TextUrlDecode_Click(object sender, RoutedEventArgs e)
    {
        ApplyTransform(Utils.TextTransformer.UrlDecode);
    }

    private void ApplyTransform(Func<string, string> transform)
    {
        ToolActionHelper.SetOutput(TextOutput, () => transform(TextTransformInput.Text));
    }
}

