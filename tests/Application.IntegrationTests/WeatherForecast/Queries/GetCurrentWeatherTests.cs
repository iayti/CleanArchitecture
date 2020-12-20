using System.Threading.Tasks;
using Application.WeatherForecasts.Queries.GetCurrentWeatherForecastQuery;
using FluentAssertions;
using NUnit.Framework;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.WeatherForecast.Queries
{
    public class GetCurrentWeatherTests : TestBase
    {
        [Test]
        public async Task ShouldReturnCurrentWeather()
        {
            var query = new GetCurrentWeatherForecastQuery
            {
                Id = 2172797,
                Lat = 1,
                Lon = 1,
                Q = "London%2Cuk"
            };

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.weather.Count.Should().BeGreaterThan(0);
        }
    }
}