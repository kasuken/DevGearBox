using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevGearbox.Utils;

public class CronParseResult
{
    public string Minute { get; set; } = "";
    public string Hour { get; set; } = "";
    public string DayOfMonth { get; set; } = "";
    public string Month { get; set; } = "";
    public string DayOfWeek { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> NextRuns { get; set; } = new();
    public string Error { get; set; } = "";
}

public static class CronParser
{
    private static readonly string[] DayNames = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
    private static readonly string[] MonthNames = { "", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

    public static CronParseResult Parse(string cronExpression)
    {
        var result = new CronParseResult();

        if (string.IsNullOrWhiteSpace(cronExpression))
        {
            result.Error = "CRON expression is empty";
            return result;
        }

        try
        {
            var parts = cronExpression.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 5)
            {
                result.Error = "Invalid CRON expression. Expected format: minute hour day month weekday";
                return result;
            }

            result.Minute = parts[0];
            result.Hour = parts[1];
            result.DayOfMonth = parts[2];
            result.Month = parts[3];
            result.DayOfWeek = parts[4];

            // Generate human-readable description
            result.Description = GenerateDescription(result);

            // Generate next run times (simplified)
            result.NextRuns = GenerateNextRuns(result);

            return result;
        }
        catch (Exception ex)
        {
            result.Error = $"Error parsing CRON: {ex.Message}";
            return result;
        }
    }

    private static string GenerateDescription(CronParseResult cron)
    {
        var desc = new StringBuilder("Runs ");

        // Minute
        desc.Append(DescribeField(cron.Minute, "minute", false));

        // Hour
        desc.Append(" at ");
        desc.Append(DescribeField(cron.Hour, "hour", false));

        // Day of month
        if (cron.DayOfMonth != "*" && cron.DayOfMonth != "?")
        {
            desc.Append(" on day ");
            desc.Append(DescribeField(cron.DayOfMonth, "day", false));
        }

        // Month
        if (cron.Month != "*")
        {
            desc.Append(" in ");
            desc.Append(DescribeField(cron.Month, "month", true));
        }

        // Day of week
        if (cron.DayOfWeek != "*" && cron.DayOfWeek != "?")
        {
            desc.Append(" on ");
            desc.Append(DescribeField(cron.DayOfWeek, "weekday", true));
        }

        return desc.ToString();
    }

    private static string DescribeField(string field, string fieldType, bool isNamedField)
    {
        if (field == "*")
            return "every " + fieldType;

        if (field == "?")
            return "any " + fieldType;

        if (field.Contains("/"))
        {
            var parts = field.Split('/');
            return $"every {parts[1]} {fieldType}(s)";
        }

        if (field.Contains("-"))
        {
            var parts = field.Split('-');
            if (isNamedField && fieldType == "month")
                return $"{GetMonthName(parts[0])} through {GetMonthName(parts[1])}";
            if (isNamedField && fieldType == "weekday")
                return $"{GetDayName(parts[0])} through {GetDayName(parts[1])}";
            return $"{parts[0]} through {parts[1]}";
        }

        if (field.Contains(","))
        {
            var values = field.Split(',');
            if (isNamedField && fieldType == "month")
                return string.Join(", ", values.Select(GetMonthName));
            if (isNamedField && fieldType == "weekday")
                return string.Join(", ", values.Select(GetDayName));
            return string.Join(", ", values);
        }

        if (isNamedField && fieldType == "month")
            return GetMonthName(field);
        if (isNamedField && fieldType == "weekday")
            return GetDayName(field);

        return field;
    }

    private static string GetMonthName(string month)
    {
        if (int.TryParse(month, out int m) && m >= 1 && m <= 12)
            return MonthNames[m];
        return month;
    }

    private static string GetDayName(string day)
    {
        if (int.TryParse(day, out int d) && d >= 0 && d <= 6)
            return DayNames[d];
        return day;
    }

    private static List<string> GenerateNextRuns(CronParseResult cron)
    {
        var nextRuns = new List<string>();
        var now = DateTime.Now;

        try
        {
            // Simple next run calculation (for demonstration)
            // In a production app, you'd use a proper CRON library like Cronos or NCrontab
            
            if (cron.Minute == "*" && cron.Hour == "*")
            {
                // Every minute
                for (int i = 0; i < 5; i++)
                {
                    nextRuns.Add(now.AddMinutes(i + 1).ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            else if (cron.Minute.All(char.IsDigit) && cron.Hour.All(char.IsDigit))
            {
                // Specific time daily
                int minute = int.Parse(cron.Minute);
                int hour = int.Parse(cron.Hour);
                
                var nextRun = new DateTime(now.Year, now.Month, now.Day, hour, minute, 0);
                if (nextRun <= now)
                    nextRun = nextRun.AddDays(1);

                for (int i = 0; i < 5; i++)
                {
                    nextRuns.Add(nextRun.AddDays(i).ToString("yyyy-MM-dd HH:mm:ss"));
                }
            }
            else
            {
                nextRuns.Add("Next run calculation requires advanced CRON parsing");
            }
        }
        catch
        {
            nextRuns.Add("Unable to calculate next runs for this expression");
        }

        return nextRuns;
    }

    public static string ExplainField(string value, string fieldName)
    {
        if (string.IsNullOrWhiteSpace(value))
            return "Not specified";

        if (value == "*")
            return $"Every {fieldName}";

        if (value == "?")
            return "No specific value";

        if (value.Contains("/"))
        {
            var parts = value.Split('/');
            if (parts[0] == "*")
                return $"Every {parts[1]} {fieldName}(s)";
            return $"Every {parts[1]} {fieldName}(s) starting from {parts[0]}";
        }

        if (value.Contains("-"))
        {
            var parts = value.Split('-');
            return $"From {parts[0]} to {parts[1]}";
        }

        if (value.Contains(","))
        {
            var values = value.Split(',');
            return $"At {string.Join(", ", values)}";
        }

        return $"At {value}";
    }
}

