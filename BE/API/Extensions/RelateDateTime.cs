using System.Globalization;
using API.Resources.SystemData;
using TimeZoneConverter;

namespace API.Extensions;

public static class RelateDateTime
{
    public static DateTime? ConvertToDatetime(this DateOnly? dateOnly) =>
        dateOnly?.ToDateTime(new(0));

    public static DateTime ConvertToDatetime(this DateOnly dateOnly) => dateOnly.ToDateTime(new(0));

    public static DateOnly? ConvertToDateonly(this DateTime? datetime) =>
        datetime != null ? DateOnly.FromDateTime(datetime.Value) : null;

    public static DateOnly ConvertToDateonly(this DateTime datetime) => DateOnly.FromDateTime(datetime);

    public static DateTime? ConvertToDatetimeWithOffset(this DateTime? utc, string? rawOffset)
    {
        // Validate
        if (utc == null || string.IsNullOrEmpty(rawOffset))
            return null;

        // Extract hours and minutes from the offset string
        TimeSpan offset = ParseTimeZoneOffset(rawOffset);

        // Convert UTC time to local time using the dynamic offset
        return utc + offset;

        static TimeSpan ParseTimeZoneOffset(string offsetString)
        {
            // Remove the '+' or '-' sign and split by ':'
            var sign = offsetString[0] == '+' ? 1 : -1;
            var parts = offsetString.Substring(1).Split(':');

            // Parse hours and minutes
            int hours = int.Parse(parts[0]) * sign;
            int minutes = int.Parse(parts[1]);

            return new TimeSpan(hours, minutes, 0);
        }
    }

    public static string ConvertToSystemFormat(this DateTime data)
        => data.ToString(SystemConstant.SystemFormatDatetime);

    public static DateTime? ConvertStringToDatetime(this string? source, string? format) =>
        DateTime.TryParseExact(source ?? throw new ArgumentException(nameof(source)), format ?? throw new ArgumentException(nameof(format)),
            CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDatetime)
            ? parsedDatetime
            : default;

    public static DateTime ConvertUtcToLocalTz(this DateTime utc, string tzId = SystemConstant.LocalTimeZoneId)
    {
        TimeZoneInfo localTz = TZConvert.GetTimeZoneInfo(tzId);
        return TimeZoneInfo.ConvertTimeFromUtc(utc, localTz);
    }

    public static DateTime ConvertLocalTzToUtc(this DateTime localTime, string tzId = SystemConstant.LocalTimeZoneId)
    {
        TimeZoneInfo localTz = TZConvert.GetTimeZoneInfo(tzId);
        return TimeZoneInfo.ConvertTimeToUtc(localTime, localTz);
    }
}