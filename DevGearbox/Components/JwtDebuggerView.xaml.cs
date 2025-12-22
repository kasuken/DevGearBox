using System;
using System.Windows;
using System.Windows.Controls;

namespace DevGearbox.Components;

/// <summary>
/// Interaction logic for JwtDebuggerView.xaml
/// </summary>
public partial class JwtDebuggerView : UserControl
{
    public JwtDebuggerView()
    {
        InitializeComponent();
    }

    private void JwtTokenInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        DecodeJwtToken();
    }

    private void DecodeButton_Click(object sender, RoutedEventArgs e)
    {
        DecodeJwtToken();
    }

    private void DecodeJwtToken()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(JwtTokenInput.Text))
            {
                ClearOutputs();
                return;
            }

            var result = Utils.JwtAnalyzer.DebugJwt(JwtTokenInput.Text);
            
            HeaderOutput.Text = result.Header;
            PayloadOutput.Text = result.Payload;
            SignatureOutput.Text = result.Signature;
            
            // Update status
            if (!string.IsNullOrEmpty(result.Error))
            {
                StatusText.Text = $"❌ Error: {result.Error}";
                StatusText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(206, 145, 120)); // Red
            }
            else
            {
                StatusText.Text = "✅ JWT Token decoded successfully";
                StatusText.Foreground = new System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromRgb(78, 201, 176)); // Green
            }
        }
        catch (Exception ex)
        {
            HeaderOutput.Text = "";
            PayloadOutput.Text = "";
            SignatureOutput.Text = "";
            StatusText.Text = $"❌ Error: {ex.Message}";
            StatusText.Foreground = new System.Windows.Media.SolidColorBrush(
                System.Windows.Media.Color.FromRgb(206, 145, 120)); // Red
        }
    }

    private void ClearOutputs()
    {
        HeaderOutput.Text = "";
        PayloadOutput.Text = "";
        SignatureOutput.Text = "";
        StatusText.Text = "Enter a JWT token to decode";
        StatusText.Foreground = new System.Windows.Media.SolidColorBrush(
            System.Windows.Media.Color.FromRgb(156, 220, 254)); // Cyan
    }

    private void CopyHeader_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(HeaderOutput.Text))
        {
            Clipboard.SetText(HeaderOutput.Text);
            StatusText.Text = "✅ Header copied to clipboard";
        }
    }

    private void CopyPayload_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(PayloadOutput.Text))
        {
            Clipboard.SetText(PayloadOutput.Text);
            StatusText.Text = "✅ Payload copied to clipboard";
        }
    }

    private void CopySignature_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(SignatureOutput.Text))
        {
            Clipboard.SetText(SignatureOutput.Text);
            StatusText.Text = "✅ Signature copied to clipboard";
        }
    }

    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        JwtTokenInput.Text = "";
        ClearOutputs();
    }
}

