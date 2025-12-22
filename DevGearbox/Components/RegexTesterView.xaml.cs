using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DevGearbox.Components;

/// <summary>
/// Interaction logic for RegexTesterView.xaml
/// </summary>
public partial class RegexTesterView : UserControl
{
    public RegexTesterView()
    {
        InitializeComponent();
    }

    private void RegexInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Auto-test when regex changes
        TestRegex();
    }

    private void TestInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        // Auto-test when test text changes
        TestRegex();
    }

    private void TestRegex_Click(object sender, RoutedEventArgs e)
    {
        TestRegex();
    }

    private void TestRegex()
    {
        try
        {
            var pattern = RegexInput.Text;
            var testText = TestInput.Text;

            if (string.IsNullOrEmpty(pattern))
            {
                ResultOutput.Text = "Enter a regular expression pattern to test.";
                MatchCount.Text = "Matches: 0";
                return;
            }

            if (string.IsNullOrEmpty(testText))
            {
                ResultOutput.Text = "Enter test text to match against.";
                MatchCount.Text = "Matches: 0";
                return;
            }

            var regex = new Regex(pattern);
            var matches = regex.Matches(testText);

            if (matches.Count == 0)
            {
                ResultOutput.Text = "No matches found.";
                MatchCount.Text = "Matches: 0";
                return;
            }

            // Build detailed output
            var output = new System.Text.StringBuilder();
            output.AppendLine($"Found {matches.Count} match(es):\n");

            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i];
                output.AppendLine($"Match {i + 1}:");
                output.AppendLine($"  Value: \"{match.Value}\"");
                output.AppendLine($"  Index: {match.Index}");
                output.AppendLine($"  Length: {match.Length}");

                // Show groups if any
                if (match.Groups.Count > 1)
                {
                    output.AppendLine("  Groups:");
                    for (int g = 1; g < match.Groups.Count; g++)
                    {
                        var group = match.Groups[g];
                        if (group.Success)
                        {
                            output.AppendLine($"    Group {g}: \"{group.Value}\"");
                        }
                    }
                }

                output.AppendLine();
            }

            ResultOutput.Text = output.ToString();
            MatchCount.Text = $"Matches: {matches.Count}";
        }
        catch (ArgumentException ex)
        {
            ResultOutput.Text = $"Invalid regular expression:\n{ex.Message}";
            MatchCount.Text = "Matches: 0";
        }
        catch (Exception ex)
        {
            ResultOutput.Text = $"Error:\n{ex.Message}";
            MatchCount.Text = "Matches: 0";
        }
    }

    private void ReplaceText_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            var pattern = RegexInput.Text;
            var testText = TestInput.Text;
            var replacement = ReplacementInput.Text;

            if (string.IsNullOrEmpty(pattern))
            {
                ReplaceOutput.Text = "Enter a regular expression pattern.";
                return;
            }

            if (string.IsNullOrEmpty(testText))
            {
                ReplaceOutput.Text = "Enter test text.";
                return;
            }

            var regex = new Regex(pattern);
            var result = regex.Replace(testText, replacement ?? string.Empty);
            
            ReplaceOutput.Text = result;
        }
        catch (ArgumentException ex)
        {
            ReplaceOutput.Text = $"Invalid regular expression:\n{ex.Message}";
        }
        catch (Exception ex)
        {
            ReplaceOutput.Text = $"Error:\n{ex.Message}";
        }
    }
}

