namespace Application.IntegrationTests.Cities.Commands
{
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Delete;
    using Common.Models;
    using Domain.Entities;
    using FluentAssertions;
    using Xunit;

    //using static Testing;

    public class DeleteCityTests : IClassFixture<Testing>
    {
        public Testing _testing;

        public DeleteCityTests(Testing testing)
        {
            _testing = testing;
        }

        [Fact]
        public async Task ShouldRequireValidCityId()
        {
            var command = new DeleteCityCommand { Id = 99 };

            var result = await _testing.SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Fact]
        public async Task ShouldDeleteCity()
        {
            var city = await _testing.SendAsync(new CreateCityCommand
            {
                Name = "Kayseri"
            });

            await _testing.SendAsync(new DeleteCityCommand
            {
                Id = city.Data.Id
            });

            var list = await _testing.FindAsync<City>(city.Data.Id);

            list.Should().BeNull();
        }
    }
}
