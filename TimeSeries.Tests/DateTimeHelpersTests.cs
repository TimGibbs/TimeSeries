namespace TimeSeries.Tests;

public class DateTimeHelpersTests
{
    [Fact]
    public void StartOfYear()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.StartOfYear(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(1);
            a.Day.Should().Be(1);
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void EndOfYear()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.EndOfYear(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(12);
            a.Day.Should().Be(31);
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }
    [Fact]
    public void StartOfQuarter()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.StartOfQuarter(kvp);

            // Assert
            a.Year.Should().Be(date.Year);

            switch (date.Month)
            {
                case 1:
                case 2:
                case 3:
                    a.Month.Should().Be(1);
                    break;
                case 4:
                case 5:
                case 6:
                    a.Month.Should().Be(4);
                    break;
                case 7:
                case 8:
                case 9:
                    a.Month.Should().Be(7);
                    break;
                case 10:
                case 11:
                case 12:
                    a.Month.Should().Be(10);
                    break;
            }

            a.Day.Should().Be(1);
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void EndOfQuarter()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.EndOfQuarter(kvp);

            // Assert
            a.Year.Should().Be(date.Year);

            switch (date.Month)
            {
                case 1:
                case 2:
                case 3:
                    a.Month.Should().Be(3);
                    a.Day.Should().Be(31);

                    break;
                case 4:
                case 5:
                case 6:
                    a.Month.Should().Be(6);
                    a.Day.Should().Be(30);
                    break;
                case 7:
                case 8:
                case 9:
                    a.Month.Should().Be(9);
                    a.Day.Should().Be(30);
                    break;
                case 10:
                case 11:
                case 12:
                    a.Month.Should().Be(12);
                    a.Day.Should().Be(31);
                    break;
            }
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
        
    }

    [Fact]
    public void StartOfMonth()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.StartOfMonth(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(1);
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void EndOfMonth()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.EndOfMonth(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(DateTime.DaysInMonth(date.Year, date.Month));
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void JustDate()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.JustDate(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(date.Day);
            a.Hour.Should().Be(0);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void JustHour()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.JustHour(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(date.Day);
            a.Hour.Should().Be(date.Hour);
            a.Minute.Should().Be(0);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void JustMinute()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.JustMinutes(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(date.Day);
            a.Hour.Should().Be(date.Hour);
            a.Minute.Should().Be(date.Minute);
            a.Second.Should().Be(0);
            a.Millisecond.Should().Be(0);
        }
    }

    [Fact]
    public void JustSeconds()
    {
        // Arrange
        var fixture = new Fixture();
        var dates = fixture.CreateMany<DateTime>(100);
        var val = fixture.Create<double>();

        foreach (var date in dates)
        {
            // Act
            var kvp = new KeyValuePair<DateTime, double>(date, val);
            var a = DateTimeHelpers.JustSeconds(kvp);

            // Assert
            a.Year.Should().Be(date.Year);
            a.Month.Should().Be(date.Month);
            a.Day.Should().Be(date.Day);
            a.Hour.Should().Be(date.Hour);
            a.Minute.Should().Be(date.Minute);
            a.Second.Should().Be(date.Second);
            a.Millisecond.Should().Be(0);
        }
    }
}