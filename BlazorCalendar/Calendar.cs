using System.Collections.Immutable;
using System.Globalization;

namespace BlazorCalendar;

public record
CalendarDay(
    DateOnly Date,
    bool IsCurrentMonth,
    bool IsSelected);

public record
Calendar(
    DateOnly SelectedDay) {
        public ImmutableList<CalendarDay> DisplayedDays => SelectedDay.BuildVisibleDays();
}

public static class
CalendarFunctions {

    public static Calendar
    SetToday(this Calendar calendar) => new Calendar(SelectedDay: DateOnly.FromDateTime(DateTime.Now));

    public static Calendar
    SetYesterday(this Calendar calendar) => new Calendar(SelectedDay: DateOnly.FromDateTime(DateTime.Now).AddDays(-1));

    public static Calendar
    SetWeekAgo(this Calendar calendar) => new Calendar(SelectedDay: DateOnly.FromDateTime(DateTime.Now).AddDays(-7));


    public static Calendar
    SetMonthAgo(this Calendar calendar) => new Calendar(SelectedDay: DateOnly.FromDateTime(DateTime.Now).AddMonths(-1));


    public static Calendar
    SetPreviousMonth(this Calendar calendar) => new Calendar(SelectedDay: calendar.SelectedDay.AddMonths(-1));

    public static Calendar
    SetNextMonth(this Calendar calendar) => new Calendar(SelectedDay: calendar.SelectedDay.AddMonths(1));


    public static Calendar
    SetNextYear(this Calendar calendar) => new Calendar(SelectedDay: calendar.SelectedDay.AddYears(1));

    public static Calendar
    SetPreviousYear(this Calendar calendar) => new Calendar(SelectedDay: calendar.SelectedDay.AddYears(-1));

    public static Calendar
    SetDay(this Calendar calendar, CalendarDay day) => new Calendar(SelectedDay: day.Date);

    public static DateOnly
    CalculateMonthStart(this DateOnly day) => new DateOnly(day.Year, day.Month, 1);

    public static DateOnly
    CalculateMonthEnd(this DateOnly day) => day.CalculateMonthStart().AddMonths(1).AddDays(-1);

    public static int
    CalculateStartOffset(this DateOnly day) => (((int)day.CalculateMonthStart().DayOfWeek) + 6) % 7;

    public static DateOnly
    CalculateGridStart(this DateOnly day) => day.CalculateMonthStart().AddDays(-day.CalculateStartOffset());

    public static ImmutableList<CalendarDay>
    BuildVisibleDays(this DateOnly day) =>
        Enumerable.Range(0, 42)
        .Select(i => {
            var date = day.CalculateGridStart().AddDays(i);                           //  нужно ли тут каждый раз просчитывать GridStart или его можно вынести за цикл? Влияет ли это на что-то (скорость, память и т.д.)
            return new CalendarDay(
                Date: date,
                IsCurrentMonth: date.Month == day.Month,
                IsSelected: date == day);
        })
        .ToImmutableList();
}