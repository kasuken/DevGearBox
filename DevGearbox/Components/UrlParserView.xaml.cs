using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
namespace DevGearbox.Components;
public partial class UrlParserView : UserControl
{
    public UrlParserView()
    {
        InitializeComponent();
    }
    private void UrlInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        ParseUrl();
    }
    private void ParseButton_Click(object sender, RoutedEventArgs e)
    {
        ParseUrl();
    }
    private void ParseUrl()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(UrlInput.Text))
            {
                ClearOutputs();
                StatusText.Text = "Enter a URL to parse";
                return;
            }
            var result = Utils.UrlParser.Parse(UrlInput.Text);
            if (!string.IsNullOrEmpty(result.Error))
            {
                ClearOutputs();
                StatusText.Text = $"❌ {result.Error}";
                return;
            }
            // Display URL components in individual textboxes
            SchemeText.Text = result.Scheme;
            HostText.Text = result.Host;
            PortText.Text = result.Port;
            PathText.Text = result.Path;
            QueryText.Text = result.Query;
            // Show/hide fragment based on presence
            if (!string.IsNullOrEmpty(result.Fragment))
            {
                FragmentText.Text = result.Fragment;
                FragmentBorder.Visibility = Visibility.Visible;
            }
            else
            {
                FragmentText.Text = "";
                FragmentBorder.Visibility = Visibility.Collapsed;
            }
            // Display parameters dynamically
            ParametersPanel.Children.Clear();
            if (result.Parameters.Count > 0)
            {
                foreach (var param in result.Parameters)
                {
                    CreateParameterControl(param.Key, param.Value);
                }
                NoParametersText.Visibility = Visibility.Collapsed;
                StatusText.Text = $"✅ Parsed URL with {result.Parameters.Count} parameter(s)";
            }
            else
            {
                NoParametersText.Visibility = Visibility.Visible;
                StatusText.Text = "✅ Parsed URL (no parameters)";
            }
        }
        catch (Exception ex)
        {
            StatusText.Text = $"❌ Error: {ex.Message}";
            ClearOutputs();
        }
    }
    private void CreateParameterControl(string key, string value)
    {
        var border = new Border
        {
            Background = new SolidColorBrush(Color.FromRgb(30, 30, 30)),
            BorderBrush = new SolidColorBrush(Color.FromRgb(62, 62, 66)),
            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(3),
            Padding = new Thickness(10),
            Margin = new Thickness(0, 0, 0, 10)
        };
        var grid = new Grid();
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        // Parameter name label
        var keyLabel = new TextBlock
        {
            Text = key,
            Foreground = new SolidColorBrush(Color.FromRgb(156, 220, 254)),
            FontSize = 11,
            Margin = new Thickness(0, 0, 0, 5)
        };
        Grid.SetRow(keyLabel, 0);
        Grid.SetColumn(keyLabel, 0);
        Grid.SetColumnSpan(keyLabel, 2);
        grid.Children.Add(keyLabel);
        // Parameter value textbox
        var valueTextBox = new TextBox
        {
            Text = value,
            IsReadOnly = true,
            FontFamily = new FontFamily("Consolas"),
            FontSize = 12,
            Background = new SolidColorBrush(Color.FromRgb(37, 37, 38)),
            Foreground = new SolidColorBrush(Color.FromRgb(206, 145, 120)),
            BorderThickness = new Thickness(0),
            Padding = new Thickness(5),
            TextWrapping = TextWrapping.Wrap,
            Margin = new Thickness(0, 0, 10, 0)
        };
        Grid.SetRow(valueTextBox, 1);
        Grid.SetColumn(valueTextBox, 0);
        grid.Children.Add(valueTextBox);
        // Copy button
        var copyButton = new Button
        {
            Content = "📋",
            Padding = new Thickness(8, 4, 8, 4),
            Background = new SolidColorBrush(Color.FromRgb(14, 99, 156)),
            BorderThickness = new Thickness(0),
            ToolTip = $"Copy {key} value",
            VerticalAlignment = VerticalAlignment.Center
        };
        Grid.SetRow(copyButton, 1);
        Grid.SetColumn(copyButton, 1);
        // Copy button click handler
        copyButton.Click += (s, e) =>
        {
            Clipboard.SetText(value);
            StatusText.Text = $"✅ Copied {key} value to clipboard";
        };
        grid.Children.Add(copyButton);
        border.Child = grid;
        ParametersPanel.Children.Add(border);
    }
    private void ClearOutputs()
    {
        SchemeText.Text = "";
        HostText.Text = "";
        PortText.Text = "";
        PathText.Text = "";
        QueryText.Text = "";
        FragmentText.Text = "";
        FragmentBorder.Visibility = Visibility.Collapsed;
        ParametersPanel.Children.Clear();
        NoParametersText.Visibility = Visibility.Collapsed;
    }
    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        UrlInput.Text = "";
        ClearOutputs();
        StatusText.Text = "Enter a URL to parse";
    }
}
