using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSeries;

public class TimeSeries : Dictionary<DateTime,double>
{

    public TimeSeries() : base()
    {
    }

    public TimeSeries(IDictionary<DateTime, double> dict) : base(dict)
    {
    }


    public static TimeSeries AdjustValues(TimeSeries timeSeries, Func<KeyValuePair<DateTime, double>, double> func) =>
        new(
            timeSeries.ToDictionary(
                k=>k.Key,
                func)
        );

    public static TimeSeries AdjustDates(TimeSeries timeSeries, Func<KeyValuePair<DateTime, double>, DateTime> func) =>
        new(
            timeSeries.GroupBy(func).ToDictionary(
                k=>k.Key,
                v=>v.Sum(x=>x.Value))
        );

    public static TimeSeries operator +(TimeSeries timeSeries, double value) =>
        AdjustValues(timeSeries, v => v.Value + value);

    public static TimeSeries operator +(double value, TimeSeries timeSeries) 
        => AdjustValues(timeSeries, v => value + v.Value);

    public static TimeSeries operator +(TimeSeries timeSeries1, TimeSeries timeSeries2) =>
        new(timeSeries1.Concat(timeSeries2.AsEnumerable())
            .GroupBy(o=>o.Key)
            .ToDictionary(k=>k.Key, v=>v.Sum(x=>x.Value)));

    public static TimeSeries operator -(TimeSeries timeSeries, double value) 
        => AdjustValues(timeSeries, v => v.Value - value);

    public static TimeSeries operator -(double value, TimeSeries timeSeries) 
        => AdjustValues(timeSeries, v => value - v.Value);

    public static TimeSeries operator -(TimeSeries timeSeries1, TimeSeries timeSeries2)
    {
        var t = new TimeSeries(timeSeries1);
        foreach (var point in timeSeries2)
        {
            t[point.Key] = t.TryGetValue(point.Key, out var val) ? val- point.Value : -point.Value;
        }

        return t;
    }

    public static TimeSeries operator *(TimeSeries timeSeries, double value)
        => AdjustValues(timeSeries, v => v.Value * value);

    public static TimeSeries operator *(double value, TimeSeries timeSeries)
        => AdjustValues(timeSeries, v => value * v.Value);

    public static TimeSeries operator *(TimeSeries timeSeries1, TimeSeries timeSeries2) =>
        new(timeSeries1.ToDictionary(k => k.Key,
            v => timeSeries2.TryGetValue(v.Key, out var val)
                ? val * v.Value
                : throw new KeyNotFoundException($"{nameof(timeSeries2)} does not include key:{v.Key: s}")));

    public static TimeSeries operator /(TimeSeries timeSeries, double value)
        => AdjustValues(timeSeries, v => v.Value / value);

    public static TimeSeries operator /(double value, TimeSeries timeSeries)
        => AdjustValues(timeSeries, v => value / v.Value);

    public static TimeSeries operator /(TimeSeries timeSeries1, TimeSeries timeSeries2) =>
        new(timeSeries1.ToDictionary(k => k.Key,
            v => timeSeries2.TryGetValue(v.Key, out var val)
                ? v.Value / val
                : throw new KeyNotFoundException($"{nameof(timeSeries2)} does not include key:{v.Key: s}")));

    public double[] this[IEnumerable<DateTime> dates]
    {
        get { return dates.Select(d => this[d]).ToArray(); }
        set
        {
            var array = dates.ToArray();
            if (array.Length != value.Length)
                throw new ArgumentException($"Indexed using {array.Length} dates but supplied {value.Length} values");
            for (var i = 0; i < array.Length; i++)
            {
                this[array[i]] = value[i];
            }
        }
    }

    public IEnumerable<KeyValuePair<DateTime, double>> OrderEnumerable()
    {
        return this.OrderBy(o => o.Key);
    }
    public KeyValuePair<DateTime, double> this[int i]
    {
        get
        {
            var date = Keys.OrderBy(k => k).ToArray()[i];
            return new KeyValuePair<DateTime, double>(date, this[date]);
        }
    }

    public KeyValuePair<DateTime, double> First() => OrderEnumerable().First();
    public KeyValuePair<DateTime, double> First(Func<KeyValuePair<DateTime, double>, bool> predicate) => OrderEnumerable().First(predicate);
    public KeyValuePair<DateTime, double> FirstOrDefault() => OrderEnumerable().FirstOrDefault();
    public KeyValuePair<DateTime, double> Last() => OrderEnumerable().Last();
    public KeyValuePair<DateTime, double> Last(Func<KeyValuePair<DateTime, double>, bool> predicate) => OrderEnumerable().Last(predicate);
    public KeyValuePair<DateTime, double> LastOrDefault() => OrderEnumerable().LastOrDefault();
}