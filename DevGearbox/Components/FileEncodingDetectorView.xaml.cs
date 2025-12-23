using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using DevGearbox.Utils;
using Microsoft.Win32;
using Wpf.Ui.Controls;

namespace DevGearbox.Components;

public partial class FileEncodingDetectorView : UserControl
{
    private readonly SolidColorBrush _defaultBorderBrush = new(Color.FromRgb(0x3E, 0x3E, 0x42));
    private readonly SolidColorBrush _hoverBorderBrush = new(Color.FromRgb(0x0E, 0x63, 0x9C));

    public FileEncodingDetectorView()
    {
        InitializeComponent();
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
            SetStatus("Rilascia un file valido", InfoBarSeverity.Warning);
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
            Title = "Seleziona un file da analizzare"
        };

        if (dialog.ShowDialog() == true)
        {
            ProcessFile(dialog.FileName);
        }
    }

    private void ProcessFile(string filePath)
    {
        SelectedFileText.Text = filePath;
        var detection = FileEncodingDetector.DetectEncoding(filePath);
        DetectedEncodingText.Text = detection;

        if (IsErrorMessage(detection))
        {
            SetStatus(detection, InfoBarSeverity.Error);
        }
        else
        {
            SetStatus($"Encoding rilevato: {detection}", InfoBarSeverity.Success);
        }
    }

    private static bool IsErrorMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return true;

        var lowered = message.ToLowerInvariant();
        return lowered.StartsWith("file not found", StringComparison.Ordinal) ||
               lowered.StartsWith("unable to read file", StringComparison.Ordinal) ||
               lowered.StartsWith("please provide", StringComparison.Ordinal) ||
               lowered.StartsWith("empty file", StringComparison.Ordinal);
    }

    private void SetStatus(string message, InfoBarSeverity severity)
    {
        StatusInfoBar.Title = message;
        StatusInfoBar.Severity = severity;
    }
}
