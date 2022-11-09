using System.Reflection.Metadata;
using System.Text.Json;

namespace TimeSeries.Tests;

public class TimeSeriesTests
{
    [Fact]
    public void Add_TimerSeries_Double()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = f + d;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] + d);
        }
    }

    [Fact]
    public void Add_Double_TimerSeries()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = d + f;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] + d);
        }
    }

    [Fact]
    public void Add_TimerSeries_TimeSeries()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(4).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] }, { dates[3], doubles[5] }, };
            
        var a = ts1 + ts2;

        a[dates[0]].Should().Be(doubles[0] + doubles[1]);
        a[dates[1]].Should().Be(doubles[2] + doubles[3]);
        a[dates[2]].Should().Be(doubles[4]);
        a[dates[3]].Should().Be(doubles[5]);
    }

    [Fact]
    public void Subtraction_TimerSeries_Double()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = f - d;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] - d);
        }
    }

    [Fact]
    public void Subtraction_Double_TimerSeries()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = d - f;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(d - f[timeSeries.Key]);
        }
    }

    [Fact]
    public void Subtraction_TimerSeries_TimeSeries()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(4).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] }, { dates[3], doubles[5] }, };

        var a = ts1 - ts2;

        a[dates[0]].Should().Be(doubles[0] - doubles[1]);
        a[dates[1]].Should().Be(doubles[2] - doubles[3]);
        a[dates[2]].Should().Be(doubles[4]);
        a[dates[3]].Should().Be(-doubles[5]);
    }

    [Fact]
    public void Multiply_TimerSeries_Double()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = f * d;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] * d);
        }
    }

    [Fact]
    public void Multiply_Double_TimerSeries()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = d * f;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] * d);
        }
    }

    [Fact]
    public void Multiply_TimerSeries_TimeSeries()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(3).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] }, { dates[2], doubles[5] }, };

        var a = ts1 * ts2;

        a[dates[0]].Should().Be(doubles[0] * doubles[1]);
        a[dates[1]].Should().Be(doubles[2] * doubles[3]);
        a[dates[2]].Should().Be(doubles[4] * doubles[5]);
    }

    [Fact]
    public void Multiply_TimerSeries_TimeSeries_Ts2MissingKey_Throws()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(3).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] } };

        var a = () => ts1 * ts2;

        a.Should().Throw<KeyNotFoundException>($"timeSeries2 does not include key:{dates[2]: s}");
    }

    [Fact]
    public void Divide_TimerSeries_Double()
    {
        var fixture = new Fixture();
        var f = new TimeSeries(fixture.CreateMany<KeyValuePair<DateTime, double>>()
            .ToDictionary(k => k.Key, v => v.Value));
        var d = fixture.Create<double>();

        var a = f / d;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(f[timeSeries.Key] / d);
        }
    }

    [Fact]
    public void Divide_Double_TimerSeries()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();
        var d = fixture.Create<double>();

        var a = d / f;

        foreach (var timeSeries in f)
        {
            a[timeSeries.Key].Should().Be(d / f[timeSeries.Key]);
        }
    }

    [Fact]
    public void Divide_TimerSeries_TimeSeries()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(3).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] }, { dates[2], doubles[5] }, };

        var a = ts1 / ts2;

        a[dates[0]].Should().Be(doubles[0] / doubles[1]);
        a[dates[1]].Should().Be(doubles[2] / doubles[3]);
        a[dates[2]].Should().Be(doubles[4] / doubles[5]);
    }

    [Fact]
    public void Divide_TimerSeries_TimeSeries_Ts2MissingKey_Throws()
    {
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(3).ToArray();
        var doubles = fixture.CreateMany<double>(6).ToArray();

        var ts1 = new TimeSeries() { { dates[0], doubles[0] }, { dates[1], doubles[2] }, { dates[2], doubles[4] }, };
        var ts2 = new TimeSeries() { { dates[0], doubles[1] }, { dates[1], doubles[3] } };

        var a = () => ts1 / ts2;

        a.Should().Throw<KeyNotFoundException>($"timeSeries2 does not include key:{dates[2]: s}");
    }

    [Fact]
    public void Serialization_WorksNeatly()
    {
        var fixture = new Fixture().Customize(new TestConventions());
        var f = fixture.Create<TimeSeries>();

        var s = JsonSerializer.Serialize(f);
        var a = JsonSerializer.Deserialize<TimeSeries>(s);

        a.Should().BeEquivalentTo(f);
    }


    [Fact]
    public void AdjustDates_WAI()
    {
        var fixture = new Fixture();
        var date = new DateTime(2022, 2, 13);
        var doubles = fixture.CreateMany<double>(3).ToArray();

        var ts1 = new TimeSeries() { { date, doubles[0] }, { date.AddMonths(1), doubles[1] }, { date.AddYears(1), doubles[2] }, };
        var func = (KeyValuePair<DateTime, double> kvp) => new DateTime(kvp.Key.Year, 1, 1);
        var a = TimeSeries.AdjustDates(ts1, func);
        a.Count.Should().Be(2);
        a[new DateTime(2022, 1, 1)].Should().Be(doubles[0] + doubles[1]);
        a[new DateTime(2023, 1, 1)].Should().Be(doubles[2]);
    }

    [Fact]
    public void IntIndex()
    {
        var fixture = new Fixture();
        var date = new DateTime(2022, 2, 13);
        var doubles = fixture.CreateMany<double>(3).ToArray();

        var ts1 = new TimeSeries() { { date, doubles[0] },  { date.AddYears(1), doubles[2] }, { date.AddMonths(1), doubles[1] }, };

        var secondItem = ts1[1];
        secondItem.Key.Should().Be(date.AddMonths(1));
        secondItem.Value.Should().Be(doubles[1]);
    }

    [Fact]
    public void OrderedEnumerable()
    {
        var fixture = new Fixture();
        var date = new DateTime(2022, 2, 13);
        var doubles = fixture.CreateMany<double>(3).ToArray();

        var ts1 = new TimeSeries() { { date, doubles[0] }, { date.AddYears(1), doubles[2] }, { date.AddMonths(1), doubles[1] }, };

        var a = ts1.OrderEnumerable().ToArray();
        a[0].Key.Should().Be(date);
        a[0].Value.Should().Be(doubles[0]);
        a[1].Key.Should().Be(date.AddMonths(1));
        a[1].Value.Should().Be(doubles[1]);
        a[2].Key.Should().Be(date.AddYears(1));
        a[2].Value.Should().Be(doubles[2]);
    }

    [Fact]
    public void First_Last()
    {
        var fixture = new Fixture();
        var date = new DateTime(2022, 2, 13);
        var doubles = fixture.CreateMany<double>(3).ToArray();

        var ts1 = new TimeSeries() {  { date.AddYears(1), doubles[2] }, { date.AddMonths(1), doubles[1] }, { date, doubles[0] }, };

        ts1.First().Key.Should().Be(date);
        ts1.First().Value.Should().Be(doubles[0]);

        ts1.FirstOrDefault().Should().Be(ts1.First());

        ts1.First(o => o.Key > date).Key.Should().Be(date.AddMonths(1));

        ts1.Last().Key.Should().Be(date.AddYears(1));
        ts1.Last().Value.Should().Be(doubles[2]);

        ts1.LastOrDefault().Should().Be(ts1.Last());

        ts1.Last(o => o.Key < date.AddYears(1)).Key.Should().Be(date.AddMonths(1));
    }

    [Fact]
    public void First_Last_Empty_Default()
    {
        var ts1 = new TimeSeries();

        ts1.FirstOrDefault().Key.Should().Be(default);
        ts1.FirstOrDefault().Value.Should().Be(default);

        ts1.LastOrDefault().Key.Should().Be(default);
        ts1.LastOrDefault().Value.Should().Be(default);
    }
}