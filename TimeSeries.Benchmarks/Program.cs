// See https://aka.ms/new-console-template for more information

using AutoFixture;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;


using TimeSeries;

var summary = BenchmarkRunner.Run<SumEfficiency>();

Console.ReadKey();

[MemoryDiagnoser]
public class SumEfficiency
{
    /// This benchmark shows that the Linq Sum method for an array of TimeSeries
    /// is more efficient than the concurrent dictionary approach

    [Params(100, 500, 1000)]
    public int SeriesLength { get; set; }

    [Params(10, 50, 100)]
    public int NumberOfSeries { get; set; }


    [GlobalSetup]
    public void GlobalSetup()
    {
        var fixture = new Fixture();
        fixture.Customize<KeyValuePair<DateTime,double>>(o => o.FromFactory(() =>
        {
            var d = fixture.Create<DateTime>();
            var v = fixture.Create<double>();
            return new KeyValuePair<DateTime, double>(new DateTime(d.Year, d.Month, 1),v);
        }));
        fixture.Customize<TimeSeries.TimeSeries>(o => o.FromFactory(() => new TimeSeries.TimeSeries(fixture
            .CreateMany<KeyValuePair<DateTime, double>>(SeriesLength).GroupBy(q=>q.Key)
            .ToDictionary(k => k.Key, v => v.First().Value))));
        _timeSeries = fixture.CreateMany<TimeSeries.TimeSeries>(NumberOfSeries);
    }

    private IEnumerable<TimeSeries.TimeSeries> _timeSeries { get; set; }

    [Benchmark]
    public TimeSeries.TimeSeries Sum() => _timeSeries.Sum();

    [Benchmark]
    public TimeSeries.TimeSeries Sum_CD() => _timeSeries.Sum_CD();
}
