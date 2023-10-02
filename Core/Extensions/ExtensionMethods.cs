namespace Core.Extensions
{
    public static class ExtensionMethods
    {
        public static DateTime MonthFirstDay(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }
        public static DateTime MonthLastDay(this DateTime dt)
        {
            return dt.MonthFirstDay().AddMonths(1).AddDays(-1);
        }
    }
}
