using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Cities.Commands.Create;
using CleanArchitecture.Application.Cities.Commands.Update;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Cities.Commands
{
    public class UpdateCityTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCityId()
        {
            var command = new UpdateCityCommand
            {
                Id = 99,
                Name = "Kayseri"
            };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<NotFoundException>();
        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            await RunAsDefaultUserAsync();
            
            var city = await SendAsync(new CreateCityCommand("Malatya"));

            await SendAsync(new CreateCityCommand("Denizli"));

            var command = new UpdateCityCommand
            {
                Id = city.Data.Id,
                Name = "Denizli"
            };

            FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("Name")).Result
                .And.Errors["Name"].Should().Contain("The specified city already exists. If you just want to activate the city leave the name field blank!");
        }

        [Test]
        public async Task ShouldUpdateCity()
        {
            var userId = await RunAsDefaultUserAsync();

            var result = await SendAsync(new CreateCityCommand("Kayyysseri"));

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
            city.ModifyDate.Should().BeCloseTo(DateTime.Now, 10.Seconds());
        }
    }
}
