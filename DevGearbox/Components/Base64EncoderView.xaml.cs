using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class Base64EncoderView : UserControl
{
    public Base64EncoderView()
    {
        InitializeComponent();
    }

    private void EncodeBase64_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ToolActionHelper.SetOutput(Base64Output, () => Utils.Base64Converter.Encode(Base64Input.Text));
    }

    private void DecodeBase64_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        ToolActionHelper.SetOutput(Base64Output, () => Utils.Base64Converter.Decode(Base64Input.Text));
    }
}

