namespace TimeSeries.Tests;

public class TimeSeriesExtensionsTests
{
    [Fact]
    public void ToCumulative()
    {
        var fixture = new Fixture();
        var date = new DateTime(2022, 2, 13);
        var doubles = fixture.CreateMany<double>(3).ToArray();

        var ts1 = new TimeSeries() { { date.AddYears(1), doubles[2] }, { date.AddMonths(1), doubles[1] }, { date, doubles[0] }, };

        var a = ts1.ToCumulative();

        a[date].Should().Be(doubles[0]);
        a[date.AddMonths(1)].Should().Be(doubles[0] + doubles[1]);
        a[date.AddYears(1)].Should().Be(doubles[0] + doubles[1] + doubles[2]);
    }
}