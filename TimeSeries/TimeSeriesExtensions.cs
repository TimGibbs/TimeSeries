using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeSeries;

public static class TimeSeriesExtensions
{
    public static TimeSeries ToCumulative(this TimeSeries timeSeries)
    {
        var v = 0d;
        var n = new TimeSeries();
        foreach (var timeSeriesPoint in timeSeries.OrderEnumerable())
        {
            v += timeSeriesPoint.Value;
            n[timeSeriesPoint.Key] = v;
        }

        return n;
    }

    public static TimeSeries Sum(this IEnumerable<TimeSeries> timeSeries) 
        => new(timeSeries
            .SelectMany(o => o)
            .GroupBy(k => k.Key)
            .ToDictionary(k => k.Key, 
                            v => v.Sum(o => o.Value)));

    [Obsolete]
    public static TimeSeries Sum_CD(this IEnumerable<TimeSeries> timeSeries)
    {
        var dict = new ConcurrentDictionary<DateTime, double>();

        Parallel.ForEach(timeSeries, series =>
        {
            foreach (var pair in series)
            {
                dict.AddOrUpdate(pair.Key, pair.Value, (time, currentDateTime) => currentDateTime + pair.Value);
            }
        });

        return new TimeSeries(dict);
    }
}