using System;
namespace DevGearbox.Utils;
public static class TimestampConverter
{
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    public static string UnixToDateTime(string timestamp)
    {
        if (string.IsNullOrWhiteSpace(timestamp))
            return string.Empty;
        try
        {
            if (long.TryParse(timestamp, out long unixTime))
            {
                // Handle both seconds and milliseconds
                DateTime dateTime;
                if (unixTime > 253402300799) // If larger than max seconds timestamp
                {
                    dateTime = UnixEpoch.AddMilliseconds(unixTime);
                }
                else
                {
                    dateTime = UnixEpoch.AddSeconds(unixTime);
                }
                return $"UTC: {dateTime:yyyy-MM-dd HH:mm:ss}\nLocal: {dateTime.ToLocalTime():yyyy-MM-dd HH:mm:ss}";
            }
            else
            {
                return "Invalid timestamp format";
            }
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
    public static string DateTimeToUnix(DateTime dateTime)
    {
        try
        {
            var utcDateTime = dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime();
            long unixTimeSeconds = (long)(utcDateTime - UnixEpoch).TotalSeconds;
            long unixTimeMilliseconds = (long)(utcDateTime - UnixEpoch).TotalMilliseconds;
            return $"Seconds: {unixTimeSeconds}\nMilliseconds: {unixTimeMilliseconds}";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
