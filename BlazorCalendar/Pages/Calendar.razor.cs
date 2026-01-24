using System.Collections.Immutable;

namespace BlazorCalendar.Pages;

public partial record
CalendarDay(
    DateOnly Date,
    bool IsCurrentMonth,               
    bool IsSelected);
    
public partial class
Calendar {

    protected DateOnly selectedDay = DateOnly.FromDateTime(DateTime.Today);
    protected ImmutableList<CalendarDay> calendar = DateOnly.FromDateTime(DateTime.Today).BuildGridDays();
    protected bool isCalendarOpened = false;

    protected void
    SwitchCalendarState() {
        isCalendarOpened = !isCalendarOpened;
    }

    protected void
    SetToday() {
        selectedDay = DateOnly.FromDateTime(DateTime.Today);
        calendar = selectedDay.BuildGridDays();
    }

    protected void
    SetYesterday() {
        selectedDay = selectedDay.AddDays(-1);
        calendar = selectedDay.BuildGridDays();   // сначала я подумал, что можно просто calendar = selectedDay.AddDays(-1).BuildGridDays(); , но потом дошло, что так не меняется состояние selectedDay и кнопку получается кликабельна только один раз.
    }

    protected void
    SetWeekAgo() {
        selectedDay = selectedDay.AddDays(-7);
        calendar = selectedDay.BuildGridDays();
    }

    protected void
    SetMonthAgo() {
        selectedDay = selectedDay.AddMonths(-1);
        calendar = selectedDay.BuildGridDays();
    }

    protected void
    SetNextMonth() {
        selectedDay = selectedDay.AddMonths(1);
        calendar = selectedDay.BuildGridDays();
    }

    protected void
    SetNextYear() {
        selectedDay = selectedDay.AddYears(1);
        calendar = selectedDay.BuildGridDays();
    }
    protected void
    SetYearAgo() {
        selectedDay = selectedDay.AddYears(-1);
        calendar = selectedDay.BuildGridDays();
    }

    protected void
    SetDay(CalendarDay selectedDay) {
        this.selectedDay = selectedDay.Date;
        calendar = this.selectedDay.BuildGridDays();
    }

}
    
public static partial class
CalendarExtensions {

    public static DateOnly
    CalculateMonthStart(this DateOnly day) => new DateOnly(day.Year, day.Month, 1);

    public static DateOnly
    CalculateMonthEnd(this DateOnly day) => day.CalculateMonthStart().AddMonths(1).AddDays(-1);

    public static int
    CalculateStartOffset(this DateOnly day) => (((int)day.CalculateMonthStart().DayOfWeek) + 6) % 7;

    public static DateOnly
    CalculateGridStart(this DateOnly day) => day.CalculateMonthStart().AddDays(-day.CalculateStartOffset());

    public static ImmutableList<CalendarDay>
    BuildGridDays(this DateOnly day) =>
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