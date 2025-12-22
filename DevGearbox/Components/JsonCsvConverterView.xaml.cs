using System;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace DevGearbox.Components;

public partial class JsonCsvConverterView : UserControl
{
    public JsonCsvConverterView()
    {
        InitializeComponent();
    }

    private void JsonToCsv_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(InputText.Text))
            {
                StatusInfoBar.Title = "Please enter JSON data";
                StatusInfoBar.Severity = InfoBarSeverity.Warning;
                return;
            }
            var csv = Utils.JsonCsvConverter.JsonToCsv(InputText.Text);
            OutputText.Text = csv;
            StatusInfoBar.Title = "Successfully converted JSON to CSV";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            OutputText.Text = "";
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
    }

    private void CsvToJson_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(InputText.Text))
            {
                StatusInfoBar.Title = "Please enter CSV data";
                StatusInfoBar.Severity = InfoBarSeverity.Warning;
                return;
            }
            var json = Utils.JsonCsvConverter.CsvToJson(InputText.Text, prettyPrint: true);
            OutputText.Text = json;
            StatusInfoBar.Title = "Successfully converted CSV to JSON";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
        catch (Exception ex)
        {
            OutputText.Text = "";
            StatusInfoBar.Title = $"Error: {ex.Message}";
            StatusInfoBar.Severity = InfoBarSeverity.Error;
        }
    }

    private void CopyOutput_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(OutputText.Text))
        {
            Clipboard.SetText(OutputText.Text);
            StatusInfoBar.Title = "Output copied to clipboard";
            StatusInfoBar.Severity = InfoBarSeverity.Success;
        }
    }

    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        InputText.Text = "";
        OutputText.Text = "";
        StatusInfoBar.Title = "Ready to convert between JSON and CSV";
        StatusInfoBar.Severity = InfoBarSeverity.Informational;
    }

    private void SwapInputOutput_Click(object sender, RoutedEventArgs e)
    {
        var temp = InputText.Text;
        InputText.Text = OutputText.Text;
        OutputText.Text = temp;
        StatusInfoBar.Title = "Swapped input and output";
        StatusInfoBar.Severity = InfoBarSeverity.Informational;
    }
}
