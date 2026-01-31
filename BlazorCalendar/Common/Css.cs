namespace BlazorCalendar;

public static class Css { 

    public static string
    T (this bool value, string @class) => value ? @class : string.Empty;
    
    public static string
    F (this bool value, string @class) => !value ? @class : string.Empty;

}
