using System.Windows;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class HashGeneratorView : UserControl
{
    public HashGeneratorView()
    {
        InitializeComponent();
    }
    private void GenerateMD5_Click(object sender, RoutedEventArgs e)
    {
        HashOutput.Text = Utils.HashGenerator.GenerateMD5(HashInput.Text);
    }
    private void GenerateSHA1_Click(object sender, RoutedEventArgs e)
    {
        HashOutput.Text = Utils.HashGenerator.GenerateSHA1(HashInput.Text);
    }
    private void GenerateSHA256_Click(object sender, RoutedEventArgs e)
    {
        HashOutput.Text = Utils.HashGenerator.GenerateSHA256(HashInput.Text);
    }
    private void GenerateSHA512_Click(object sender, RoutedEventArgs e)
    {
        HashOutput.Text = Utils.HashGenerator.GenerateSHA512(HashInput.Text);
    }
}
