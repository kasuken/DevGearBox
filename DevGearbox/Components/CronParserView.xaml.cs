using System;
using System.Windows;
using System.Windows.Controls;
namespace DevGearbox.Components;
public partial class CronParserView : UserControl
{
    public CronParserView()
    {
        InitializeComponent();
    }
    private void CronInput_TextChanged(object sender, TextChangedEventArgs e)
    {
        ParseCron();
    }
    private void ParseButton_Click(object sender, RoutedEventArgs e)
    {
        ParseCron();
    }
    private void ParseCron()
    {
        try
        {
            if (string.IsNullOrWhiteSpace(CronInput.Text))
            {
                ClearOutputs();
                StatusText.Text = "Enter a CRON expression to parse";
                return;
            }
            var result = Utils.CronParser.Parse(CronInput.Text);
            if (!string.IsNullOrEmpty(result.Error))
            {
                ClearOutputs();
                StatusText.Text = $"❌ {result.Error}";
                return;
            }
            // Display CRON fields
            MinuteText.Text = result.Minute;
            MinuteExplain.Text = Utils.CronParser.ExplainField(result.Minute, "minute");
            HourText.Text = result.Hour;
            HourExplain.Text = Utils.CronParser.ExplainField(result.Hour, "hour");
            DayOfMonthText.Text = result.DayOfMonth;
            DayOfMonthExplain.Text = Utils.CronParser.ExplainField(result.DayOfMonth, "day");
            MonthText.Text = result.Month;
            MonthExplain.Text = Utils.CronParser.ExplainField(result.Month, "month");
            DayOfWeekText.Text = result.DayOfWeek;
            DayOfWeekExplain.Text = Utils.CronParser.ExplainField(result.DayOfWeek, "day of week");
            // Display description
            DescriptionText.Text = result.Description;
            // Display next runs
            if (result.NextRuns.Count > 0)
            {
                NextRunsText.Text = string.Join("\n", result.NextRuns);
            }
            else
            {
                NextRunsText.Text = "Unable to calculate next runs";
            }
            StatusText.Text = "✅ CRON expression parsed successfully";
        }
        catch (Exception ex)
        {
            StatusText.Text = $"❌ Error: {ex.Message}";
            ClearOutputs();
        }
    }
    private void ClearOutputs()
    {
        MinuteText.Text = "";
        MinuteExplain.Text = "";
        HourText.Text = "";
        HourExplain.Text = "";
        DayOfMonthText.Text = "";
        DayOfMonthExplain.Text = "";
        MonthText.Text = "";
        MonthExplain.Text = "";
        DayOfWeekText.Text = "";
        DayOfWeekExplain.Text = "";
        DescriptionText.Text = "";
        NextRunsText.Text = "";
    }
    private void ClearAll_Click(object sender, RoutedEventArgs e)
    {
        CronInput.Text = "";
        ClearOutputs();
        StatusText.Text = "Enter a CRON expression to parse";
    }
    private void UseExample1_Click(object sender, RoutedEventArgs e)
    {
        CronInput.Text = "0 0 * * *";
    }
    private void UseExample2_Click(object sender, RoutedEventArgs e)
    {
        CronInput.Text = "*/15 * * * *";
    }
    private void UseExample3_Click(object sender, RoutedEventArgs e)
    {
        CronInput.Text = "0 9 * * 1-5";
    }
    private void UseExample4_Click(object sender, RoutedEventArgs e)
    {
        CronInput.Text = "30 14 1 * *";
    }
}
