namespace Application.IntegrationTests.Cities.Commands
{
    using System;
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Update;
    using Common.Models;
    using Domain.Entities;
    using FluentAssertions;
    using Xunit;

    public class UpdateCityTests : IClassFixture<Testing>
    {
        public Testing _testing;

        public UpdateCityTests(Testing testing)
        {
            _testing = testing;
        }

        [Fact]
        public async Task ShouldRequireValidCityId()
        {
            var command = new UpdateCityCommand
            {
                Id = 99,
                Name = "Kayseri"
            };

            var result = await _testing.SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Fact]
        public async Task ShouldRequireUniqueName()
        {
            var city = await _testing.SendAsync(new CreateCityCommand
            {
                Name = "Malatya"
            });

            await _testing.SendAsync(new CreateCityCommand
            {
                Name = "Denizli"
            });

            var command = new UpdateCityCommand
            {
                Id = city.Data.Id,
                Name = "Denizli"
            };

            var result = await _testing.SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Fact]
        public async Task ShouldUpdateCity()
        {
            var userId = await _testing.RunAsDefaultUserAsync();

            var result = await _testing.SendAsync(new CreateCityCommand
            {
                Name = "Kayyysseri"
            });

            var command = new UpdateCityCommand
            {
                Id = result.Data.Id,
                Name = "Kayseri"
            };

            await _testing.SendAsync(command);

            var city = await _testing.FindAsync<City>(result.Data.Id);

            city.Name.Should().Be(command.Name);
            city.Modifier.Should().NotBeNull();
            city.Modifier.Should().Be(userId);
            city.ModifyDate.Should().NotBeNull();
            city.ModifyDate.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
