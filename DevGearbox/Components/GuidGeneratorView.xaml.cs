using System;
using System.Windows;
using System.Windows.Controls;

namespace DevGearbox.Components;

/// <summary>
/// Interaction logic for GuidGeneratorView.xaml
/// </summary>
public partial class GuidGeneratorView : UserControl
{
    private Guid _currentGuid;

    public GuidGeneratorView()
    {
        InitializeComponent();
    }

    private void GenerateGuid_Click(object sender, RoutedEventArgs e)
    {
        GenerateAllGuidFormats();
    }

    private void GenerateAllGuidFormats()
    {
        // Generate a new GUID
        _currentGuid = Guid.NewGuid();

        // Update all format textboxes
        FormatN_Output.Text = _currentGuid.ToString("N");
        FormatD_Output.Text = _currentGuid.ToString("D");
        FormatB_Output.Text = _currentGuid.ToString("B");
        FormatP_Output.Text = _currentGuid.ToString("P");
        FormatX_Output.Text = _currentGuid.ToString("X");
        DefaultFormat_Output.Text = _currentGuid.ToString();
        Base64_Output.Text = Convert.ToBase64String(_currentGuid.ToByteArray());
        UpperN_Output.Text = _currentGuid.ToString("N").ToUpper();
        UpperD_Output.Text = _currentGuid.ToString("D").ToUpper();

        StatusText.Text = $"✅ Generated new GUID in all 9 formats";
    }

    private void CopyFormatN_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(FormatN_Output.Text, "Format N");
    }

    private void CopyFormatD_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(FormatD_Output.Text, "Format D");
    }

    private void CopyFormatB_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(FormatB_Output.Text, "Format B");
    }

    private void CopyFormatP_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(FormatP_Output.Text, "Format P");
    }

    private void CopyFormatX_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(FormatX_Output.Text, "Format X");
    }

    private void CopyDefault_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(DefaultFormat_Output.Text, "Default Format");
    }

    private void CopyBase64_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(Base64_Output.Text, "Base64");
    }

    private void CopyUpperN_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(UpperN_Output.Text, "Uppercase N");
    }

    private void CopyUpperD_Click(object sender, RoutedEventArgs e)
    {
        CopyToClipboard(UpperD_Output.Text, "Uppercase D");
    }

    private void CopyToClipboard(string text, string formatName)
    {
        if (!string.IsNullOrEmpty(text))
        {
            Clipboard.SetText(text);
            StatusText.Text = $"✅ Copied {formatName} to clipboard";
        }
    }

    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        FormatN_Output.Text = "";
        FormatD_Output.Text = "";
        FormatB_Output.Text = "";
        FormatP_Output.Text = "";
        FormatX_Output.Text = "";
        DefaultFormat_Output.Text = "";
        Base64_Output.Text = "";
        UpperN_Output.Text = "";
        UpperD_Output.Text = "";
        StatusText.Text = "Ready to generate GUIDs";
    }
}

