namespace Enrolly.EduDictionary.Application.Services.Implementations;

public static class DateChecker
{
    /// <summary>
    /// Возвращает true, если разница не больше одной секунды
    /// </summary>
    public static bool IsSame(DateTime date1, DateTime date2) =>
        Math.Abs((date1 - date2).Seconds) < 1;
}