namespace Application.IntegrationTests.Cities.Queries
{
    using System.Threading.Tasks;
    using Application.Cities.Queries.GetCities;
    using FluentAssertions;
    using Xunit;
    using static Testing;

    public class GetAllCitiesTests :TestBase
    {
        [Fact]
        public async Task ShouldReturnAllCities()
        {
            var query = new GetAllCitiesQuery();

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.Count.Should().BeGreaterThan(0);
        }
    }
}