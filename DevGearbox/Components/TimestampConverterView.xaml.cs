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
        UnixToDateTimeOutput.Text = Utils.TimestampConverter.UnixToDateTime(UnixTimestampInput.Text);
    }
    private void DateTimeToUnix_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (DatePickerInput.SelectedDate.HasValue)
            {
                var date = DatePickerInput.SelectedDate.Value;
                var timeParts = TimeInput.Text.Split(':');
                if (timeParts.Length == 3 &&
                    int.TryParse(timeParts[0], out int hours) &&
                    int.TryParse(timeParts[1], out int minutes) &&
                    int.TryParse(timeParts[2], out int seconds))
                {
                    var dateTime = new DateTime(date.Year, date.Month, date.Day, hours, minutes, seconds);
                    DateTimeToUnixOutput.Text = Utils.TimestampConverter.DateTimeToUnix(dateTime);
                }
                else
                {
                    DateTimeToUnixOutput.Text = "Invalid time format. Use HH:mm:ss";
                }
            }
            else
            {
                DateTimeToUnixOutput.Text = "Please select a date";
            }
        }
        catch (Exception ex)
        {
            DateTimeToUnixOutput.Text = $"Error: {ex.Message}";
        }
    }
}
