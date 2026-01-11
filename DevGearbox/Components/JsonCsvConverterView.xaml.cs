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
        if (!ToolActionHelper.RequireInput(InputText.Text, message => UpdateStatus(message, InfoBarSeverity.Warning), "Please enter JSON data"))
        {
            return;
        }

        ToolActionHelper.SetOutput(
            OutputText,
            () => Utils.JsonCsvConverter.JsonToCsv(InputText.Text),
            () => UpdateStatus("Successfully converted JSON to CSV", InfoBarSeverity.Success),
            error => UpdateStatus($"Error: {error}", InfoBarSeverity.Error));
    }

    private void CsvToJson_Click(object sender, RoutedEventArgs e)
    {
        if (!ToolActionHelper.RequireInput(InputText.Text, message => UpdateStatus(message, InfoBarSeverity.Warning), "Please enter CSV data"))
        {
            return;
        }

        ToolActionHelper.SetOutput(
            OutputText,
            () => Utils.JsonCsvConverter.CsvToJson(InputText.Text, prettyPrint: true),
            () => UpdateStatus("Successfully converted CSV to JSON", InfoBarSeverity.Success),
            error => UpdateStatus($"Error: {error}", InfoBarSeverity.Error));
    }


    private void CopyOutput_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(OutputText.Text))
        {
            Clipboard.SetText(OutputText.Text);
            UpdateStatus("Output copied to clipboard", InfoBarSeverity.Success);
        }
    }

    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        InputText.Text = "";
        OutputText.Text = "";
        UpdateStatus("Ready to convert between JSON and CSV", InfoBarSeverity.Informational);
    }


    private void SwapInputOutput_Click(object sender, RoutedEventArgs e)
    {
        var temp = InputText.Text;
        InputText.Text = OutputText.Text;
        OutputText.Text = temp;
        UpdateStatus("Swapped input and output", InfoBarSeverity.Informational);
    }

    private void UpdateStatus(string title, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = title;
        StatusInfoBar.Severity = severity;
    }
}

