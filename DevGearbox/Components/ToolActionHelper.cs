using System;
using System.Windows;
using System.Windows.Controls;

namespace DevGearbox.Components;

internal static class ToolActionHelper
{
    internal static bool RequireInput(string? input, Action<string> onMissing, string message = "Input is required.")
    {
        if (!string.IsNullOrWhiteSpace(input))
        {
            return true;
        }

        onMissing(message);
        return false;
    }

    internal static void SetOutput(TextBox target, Func<string> action, Action? onSuccess = null, Action<string>? onError = null)
    {
        if (target == null)
        {
            throw new ArgumentNullException(nameof(target));
        }

        try
        {
            var text = action();
            target.Text = text;
            onSuccess?.Invoke();
        }
        catch (Exception ex)
        {
            target.Text = string.Empty;
            (onError ?? DefaultErrorHandler)(ex.Message);
        }
    }

    internal static void Run(Action action, Action<string>? onError = null)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            (onError ?? DefaultErrorHandler)(ex.Message);
        }
    }

    private static void DefaultErrorHandler(string message)
    {
        MessageBox.Show(message, "DevGearbox", MessageBoxButton.OK, MessageBoxImage.Error);
    }
}
