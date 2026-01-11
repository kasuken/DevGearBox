using System;
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
        GenerateHash(Utils.HashGenerator.GenerateMD5);
    }

    private void GenerateSHA1_Click(object sender, RoutedEventArgs e)
    {
        GenerateHash(Utils.HashGenerator.GenerateSHA1);
    }

    private void GenerateSHA256_Click(object sender, RoutedEventArgs e)
    {
        GenerateHash(Utils.HashGenerator.GenerateSHA256);
    }

    private void GenerateSHA512_Click(object sender, RoutedEventArgs e)
    {
        GenerateHash(Utils.HashGenerator.GenerateSHA512);
    }

    private void GenerateHash(Func<string, string> generator)
    {
        ToolActionHelper.SetOutput(HashOutput, () => generator(HashInput.Text));
    }
}

