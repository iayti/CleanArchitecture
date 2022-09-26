using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Cities.Commands.Create;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Districts.Commands.Create;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Districts.Commands
{
    public class CreateDistrictTests
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateDistrictCommand();

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        [Test]
        public async Task ShouldCreateDistrict()
        {
            var rand = new Random();
            var city = await SendAsync(new CreateCityCommand($"Bursa.{rand.Next()}"));

            var userId = await RunAsDefaultUserAsync();

            var command = new CreateDistrictCommand
            {
                Name = "Karacabey",
                CityId = city.Data.Id
            };

            var result = await SendAsync(command);

            var list = await FindAsync<District>(result.Data.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
            list.Creator.Should().Be(userId);
            list.CreateDate.Should().BeCloseTo(DateTime.Now, 10.Seconds());
        }
    }
}
