using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class Base64EncoderView : UserControl
{
    public Base64EncoderView()
    {
        InitializeComponent();
    }
    private void Base64Input_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
    {
    }
    private void EncodeBase64_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Base64Output.Text = Utils.Base64Converter.Encode(Base64Input.Text);
    }
    private void DecodeBase64_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        Base64Output.Text = Utils.Base64Converter.Decode(Base64Input.Text);
    }
}
