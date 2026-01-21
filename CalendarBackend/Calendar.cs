using System.Collections.Immutable;

public record
CalendarDay(
    DateOnly Date,
    bool IsCurrentMonth,                // не стал создавать отдельный файл для этого, наверное мало смысла
    bool IsSelected);                   // не уверен, что IsSelected должен быть здесь, а не в UI части, но оставил тут пока. Наверное логинее тут, т.к. мы эту дату можем же использовать для расчета фильтров или дат               


public static class
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