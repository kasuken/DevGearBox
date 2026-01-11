using System;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class FormattersView : UserControl

{
    public FormattersView()
    {
        InitializeComponent();
    }
    private void FormatJson_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RunJson(Utils.JsonFormatter.Format);
    }

    private void MinifyJson_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RunJson(Utils.JsonFormatter.Minify);
    }

    private void FormatXml_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RunXml(Utils.XmlFormatter.Format);
    }

    private void MinifyXml_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        RunXml(Utils.XmlFormatter.Minify);
    }

    private void RunJson(Func<string, string> formatter)
    {
        ToolActionHelper.SetOutput(JsonOutput, () => formatter(JsonInput.Text));
    }

    private void RunXml(Func<string, string> formatter)
    {
        ToolActionHelper.SetOutput(XmlOutput, () => formatter(XmlInput.Text));
    }
}

