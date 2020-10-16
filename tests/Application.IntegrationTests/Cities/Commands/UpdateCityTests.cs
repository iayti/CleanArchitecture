namespace Application.IntegrationTests.Cities.Commands
{
    using System;
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Update;
    using Common.Exceptions;
    using Common.Models;
    using Domain.Entities;
    using FluentAssertions;
    using NUnit.Framework;
    using static Testing;
    public class UpdateCityTests : TestBase
    {
        [Test]
        public async Task ShouldRequireValidCityId()
        {
            var command = new UpdateCityCommand
            {
                Id = 99,
                Name = "Kayseri"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
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

            FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should().Throw<ValidationException>().Where(ex => ex.Errors.ContainsKey("Name"))
                .And.Errors["Name"].Should().Contain("The specified city already exists. If you just want to activate the city leave the name field blank!");
        }

        [Test]
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
