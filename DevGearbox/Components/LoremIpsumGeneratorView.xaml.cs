using System;
using System.Windows;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class LoremIpsumGeneratorView : UserControl
{
    public LoremIpsumGeneratorView()
    {
        InitializeComponent();
    }
    private void LengthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (CustomPanel == null) return;
        // Show custom count input only when Custom is selected (index 4)
        CustomPanel.Visibility = LengthComboBox.SelectedIndex == 4 
            ? Visibility.Visible 
            : Visibility.Collapsed;
    }
    private void Generate_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var selectedIndex = LengthComboBox.SelectedIndex;
            string generatedText = "";
            switch (selectedIndex)
            {
                case 0: // Short
                    generatedText = Utils.LoremIpsumGenerator.Generate(
                        Utils.LoremIpsumGenerator.TextLength.Short);
                    StatusText.Text = "✅ Generated 1 paragraph";
                    break;
                case 1: // Medium
                    generatedText = Utils.LoremIpsumGenerator.Generate(
                        Utils.LoremIpsumGenerator.TextLength.Medium);
                    StatusText.Text = "✅ Generated 3 paragraphs";
                    break;
                case 2: // Long
                    generatedText = Utils.LoremIpsumGenerator.Generate(
                        Utils.LoremIpsumGenerator.TextLength.Long);
                    StatusText.Text = "✅ Generated 5 paragraphs";
                    break;
                case 3: // Very Long
                    generatedText = Utils.LoremIpsumGenerator.Generate(
                        Utils.LoremIpsumGenerator.TextLength.VeryLong);
                    StatusText.Text = "✅ Generated 10 paragraphs";
                    break;
                case 4: // Custom
                    if (int.TryParse(CustomCountText.Text, out int customCount) && customCount > 0)
                    {
                        generatedText = Utils.LoremIpsumGenerator.Generate(
                            Utils.LoremIpsumGenerator.TextLength.Custom, 
                            customCount);
                        StatusText.Text = $"✅ Generated {customCount} paragraph(s)";
                    }
                    else
                    {
                        StatusText.Text = "❌ Please enter a valid number of paragraphs";
                        return;
                    }
                    break;
            }
            OutputText.Text = generatedText;
        }
        catch (Exception ex)
        {
            StatusText.Text = $"❌ Error: {ex.Message}";
        }
    }
    private void Copy_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(OutputText.Text))
        {
            Clipboard.SetText(OutputText.Text);
            StatusText.Text = "✅ Text copied to clipboard";
        }
        else
        {
            StatusText.Text = "⚠️ No text to copy. Generate some text first.";
        }
    }
    private void Clear_Click(object sender, RoutedEventArgs e)
    {
        OutputText.Text = "";
        LengthComboBox.SelectedIndex = 0;
        CustomCountText.Text = "1";
        StatusText.Text = "Select a length and click Generate";
    }
}
