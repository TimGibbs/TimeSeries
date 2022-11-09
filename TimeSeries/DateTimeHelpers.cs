using System;
using System.Collections.Generic;

namespace TimeSeries;

public static class DateTimeHelpers
{
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> StartOfYear 
        = pair => new DateTime(pair.Key.Year, 1, 1);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> EndOfYear 
        = pair => new DateTime(pair.Key.Year, 12, 31);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> StartOfQuarter 
        = pair => new DateTime(pair.Key.Year, (pair.Key.Month+2)/3*3-2, 1);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> EndOfQuarter 
        = pair =>
        {
            var m = (pair.Key.Month + 2) / 3 * 3;
            return new DateTime(pair.Key.Year, m, DateTime.DaysInMonth(pair.Key.Year, m));
        };
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> StartOfMonth 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month, 1);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> EndOfMonth 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month,DateTime.DaysInMonth(pair.Key.Year, pair.Key.Month));

    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> JustDate 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month, pair.Key.Day);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> JustHour 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month, pair.Key.Day, pair.Key.Hour,0,0);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> JustMinutes 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month, pair.Key.Day, pair.Key.Hour,pair.Key.Minute,0);
    public static readonly Func<KeyValuePair<DateTime, double>, DateTime> JustSeconds 
        = pair => new DateTime(pair.Key.Year, pair.Key.Month, pair.Key.Day, pair.Key.Hour,pair.Key.Minute,pair.Key.Second);
}