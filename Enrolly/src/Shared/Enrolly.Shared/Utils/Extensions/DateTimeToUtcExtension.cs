namespace Enrolly.Shared.Logging;

public static class DateTimeToUtcExtension
{
    public static DateTime ToUtc(this DateTime dateTime)
    {
        return dateTime.Kind switch
        {
            DateTimeKind.Local => dateTime.ToUniversalTime(),
            DateTimeKind.Unspecified => DateTime.SpecifyKind(dateTime, DateTimeKind.Utc),
            _ => dateTime
        };
    }

    public static DateTime? ToUtc(this DateTime? dateTime)
    {
        if (dateTime.HasValue) return dateTime.ToUtc();
        return null;
    }
}
