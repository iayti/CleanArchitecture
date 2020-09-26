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
    using static Testing;
    public class UpdateCityTests : TestBase
    {

        [Fact]
        public async Task ShouldRequireValidCityId()
        {
            var command = new UpdateCityCommand
            {
                Id = 99,
                Name = "Kayseri"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Fact]
        public async Task ShouldRequireUniqueName()
        {
            var city = await SendAsync(new CreateCityCommand
            {
                Name = "Malatya"
            });

            await SendAsync(new CreateCityCommand
            {
                Name = "Denizli"
            });

            var command = new UpdateCityCommand
            {
                Id = city.Data.Id,
                Name = "Denizli"
            };

            var result = await SendAsync(command);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeFalse();
            result.Error.Should().Be(ServiceError.NotFount);
        }

        [Fact]
        public async Task ShouldUpdateCity()
        {
            var userId = await RunAsDefaultUserAsync();

            var result = await SendAsync(new CreateCityCommand
            {
                Name = "Kayyysseri"
            });

            var command = new UpdateCityCommand
            {
                Id = result.Data.Id,
                Name = "Kayseri"
            };

            await SendAsync(command);

            var city = await FindAsync<City>(result.Data.Id);

            city.Name.Should().Be(command.Name);
            city.Modifier.Should().NotBeNull();
            city.Modifier.Should().Be(userId);
            city.ModifyDate.Should().NotBeNull();
            city.ModifyDate.Should().BeCloseTo(DateTime.Now, 1000);
        }
    }
}
