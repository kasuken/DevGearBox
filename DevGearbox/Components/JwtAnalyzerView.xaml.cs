using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class JwtAnalyzerView : UserControl
{
    public JwtAnalyzerView()
    {
        InitializeComponent();
    }
    private void AnalyzeJwt_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        JwtOutput.Text = Utils.JwtAnalyzer.Analyze(JwtInput.Text);
    }
}
