using System;
using System.Windows;
using System.Windows.Controls;
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
                StatusText.Text = "Please enter JSON data";
                return;
            }
            var csv = Utils.JsonCsvConverter.JsonToCsv(InputText.Text);
            OutputText.Text = csv;
            StatusText.Text = "Successfully converted JSON to CSV";
        }
        catch (Exception ex)
        {
            OutputText.Text = "";
            StatusText.Text = $"Error: {ex.Message}";
        }
    }
    private void CsvToJson_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(InputText.Text))
            {
                StatusText.Text = "Please enter CSV data";
                return;
            }
            var json = Utils.JsonCsvConverter.CsvToJson(InputText.Text, prettyPrint: true);
            OutputText.Text = json;
            StatusText.Text = "Successfully converted CSV to JSON";
        }
        catch (Exception ex)
        {
            OutputText.Text = "";
            StatusText.Text = $"Error: {ex.Message}";
        }
    }
    private void CopyOutput_Click(object sender, RoutedEventArgs e)
    {
        if (!string.IsNullOrEmpty(OutputText.Text))
        {
            Clipboard.SetText(OutputText.Text);
            StatusText.Text = "Output copied to clipboard";
        }
    }
    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        InputText.Text = "";
        OutputText.Text = "";
        StatusText.Text = "Ready to convert between JSON and CSV";
    }
    private void SwapInputOutput_Click(object sender, RoutedEventArgs e)
    {
        var temp = InputText.Text;
        InputText.Text = OutputText.Text;
        OutputText.Text = temp;
        StatusText.Text = "Swapped input and output";
    }
}
