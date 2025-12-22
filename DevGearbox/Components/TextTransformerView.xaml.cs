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
        TextOutput.Text = Utils.TextTransformer.ToUpperCase(TextTransformInput.Text);
    }
    private void TextLowerCase_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.ToLowerCase(TextTransformInput.Text);
    }
    private void TextPascalCase_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.ToPascalCase(TextTransformInput.Text);
    }
    private void TextCamelCase_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.ToCamelCase(TextTransformInput.Text);
    }
    private void TextSnakeCase_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.ToSnakeCase(TextTransformInput.Text);
    }
    private void TextKebabCase_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.ToKebabCase(TextTransformInput.Text);
    }
    private void TextReverse_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.Reverse(TextTransformInput.Text);
    }
    private void TextRemoveWhitespace_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.RemoveWhitespace(TextTransformInput.Text);
    }
    private void TextUrlEncode_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.UrlEncode(TextTransformInput.Text);
    }
    private void TextUrlDecode_Click(object sender, RoutedEventArgs e)
    {
        TextOutput.Text = Utils.TextTransformer.UrlDecode(TextTransformInput.Text);
    }
}
