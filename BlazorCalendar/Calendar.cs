using System.Collections.Immutable;
using System.Globalization;

namespace BlazorCalendar;


public static class
CalendarFunctions {

    public static DateTime
    CalculateMonthStart(this DateTime day) => new DateTime(day.Year, day.Month, 1);

    public static int
    CalculateStartOffset(this DateTime day) => (((int)day.CalculateMonthStart().DayOfWeek) + 6) % 7;

    public static DateTime
    CalculateGridStart(this DateTime day) => day.CalculateMonthStart().AddDays(-day.CalculateStartOffset());
}
    