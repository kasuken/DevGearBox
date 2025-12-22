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
        JsonOutput.Text = Utils.JsonFormatter.Format(JsonInput.Text);
    }
    private void MinifyJson_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        JsonOutput.Text = Utils.JsonFormatter.Minify(JsonInput.Text);
    }
    private void FormatXml_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        XmlOutput.Text = Utils.XmlFormatter.Format(XmlInput.Text);
    }
    private void MinifyXml_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        XmlOutput.Text = Utils.XmlFormatter.Minify(XmlInput.Text);
    }
}
