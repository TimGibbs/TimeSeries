namespace TimeSeries.Tests;

internal class TestConventions : CompositeCustomization
{
    public TestConventions() :
        base(
            new TimeSeriesCustomization())
    {

    }

    private class TimeSeriesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.Customize<TimeSeries>(o=>o.FromFactory(() => new TimeSeries(fixture.CreateMany<KeyValuePair<DateTime, double>>()
                .ToDictionary(k => k.Key, v => v.Value))));
        }
    }
}