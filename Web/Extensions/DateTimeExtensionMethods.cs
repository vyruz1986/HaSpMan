namespace Web.Extensions;

public static class DateTimeOffsetExtensionMethods
{
    public static DateTimeOffset StartOfMonth(this DateTimeOffset date)
    {
        return new DateTime(date.Year, date.Month, 1);
    }
    public static DateTimeOffset EndOfMonth(this DateTimeOffset date)
    {
        return date.StartOfMonth().AddMonths(1).AddSeconds(-1);
    }
}