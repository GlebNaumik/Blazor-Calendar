namespace BlazorCalendar.Common;

public static class DateTimeFunctions {

    public static IEnumerable<DateTime>
    GetDaysWithinWeek(this DateTime dateTime, int weeksCount) {
        var resultDaysCount = 7 * weeksCount;
        foreach (var i in Enumerable.Range(0, resultDaysCount)) {
            yield return dateTime;
            dateTime = dateTime.AddDays(1);
        }
    }
}
