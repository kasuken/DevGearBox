using System;
using System.Windows;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class TimestampConverterView : UserControl
{
    public TimestampConverterView()
    {
        InitializeComponent();
        DatePickerInput.SelectedDate = DateTime.Now;
    }
    private void UnixToDateTime_Click(object sender, RoutedEventArgs e)
    {
        ToolActionHelper.SetOutput(UnixToDateTimeOutput, () => Utils.TimestampConverter.UnixToDateTime(UnixTimestampInput.Text));
    }

    private void DateTimeToUnix_Click(object sender, RoutedEventArgs e)
    {
        ToolActionHelper.SetOutput(DateTimeToUnixOutput, () =>
        {
            if (!TryBuildDateTime(out var dateTime, out var error))
            {
                return error;
            }

            return Utils.TimestampConverter.DateTimeToUnix(dateTime);
        });
    }

    private bool TryBuildDateTime(out DateTime dateTime, out string error)
    {
        dateTime = DateTime.MinValue;
        error = string.Empty;

        if (!DatePickerInput.SelectedDate.HasValue)
        {
            error = "Please select a date";
            return false;
        }

        if (!TimeSpan.TryParse(TimeInput.Text, out var time))
        {
            error = "Invalid time format. Use HH:mm:ss";
            return false;
        }

        var date = DatePickerInput.SelectedDate.Value;
        dateTime = date.Date.Add(time);
        return true;
    }
}

