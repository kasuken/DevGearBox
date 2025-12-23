using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevGearbox.Utils;
using Microsoft.Win32;
using Wpf.Ui.Controls;

namespace DevGearbox.Components;

public partial class FileEncodingDetectorView : UserControl
{
    private readonly SolidColorBrush _defaultBorderBrush;
    private readonly SolidColorBrush _hoverBorderBrush;

    public FileEncodingDetectorView()
    {
        InitializeComponent();
        _defaultBorderBrush = TryFindResource("DropBorderBrush") as SolidColorBrush
                              ?? new SolidColorBrush(Color.FromRgb(0x3E, 0x3E, 0x42));
        _hoverBorderBrush = TryFindResource("DropHoverBrush") as SolidColorBrush
                            ?? new SolidColorBrush(Color.FromRgb(0x0E, 0x63, 0x9C));
    }

    private void DropArea_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
            DropBorder.BorderBrush = _hoverBorderBrush;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }

        e.Handled = true;
    }

    private void DropArea_DragOver(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
            DropBorder.BorderBrush = _hoverBorderBrush;
        }
        else
        {
            e.Effects = DragDropEffects.None;
            DropBorder.BorderBrush = _defaultBorderBrush;
        }

        e.Handled = true;
    }

    private void DropArea_DragLeave(object sender, DragEventArgs e)
    {
        DropBorder.BorderBrush = _defaultBorderBrush;
        e.Handled = true;
    }

    private void DropArea_Drop(object sender, DragEventArgs e)
    {
        DropBorder.BorderBrush = _defaultBorderBrush;
        e.Handled = true;

        if (!e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            SetStatus("Please drop a valid file", InfoBarSeverity.Warning);
            return;
        }

        if (e.Data.GetData(DataFormats.FileDrop) is string[] files && files.Length > 0)
        {
            ProcessFile(files[0]);
        }
    }

    private void BrowseFile_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            CheckFileExists = true,
            Multiselect = false,
            Title = "Select a file to analyze"
        };

        if (dialog.ShowDialog() == true)
        {
            ProcessFile(dialog.FileName);
        }
    }

    private void ProcessFile(string filePath)
    {
        SelectedFileText.Text = filePath;
        var (detection, isError) = FileEncodingDetector.DetectEncodingWithStatus(filePath);
        DetectedEncodingText.Text = detection;

        if (isError)
        {
            SetStatus(detection, InfoBarSeverity.Error);
        }
        else
        {
            SetStatus($"Encoding detected: {detection}", InfoBarSeverity.Success);
        }
    }

    private void SetStatus(string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = message;
        StatusInfoBar.Severity = severity;
    }
}
