using System;
using System.Threading.Tasks;
using CleanArchitecture.Application.Cities.Commands.Create;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Entities;
using FluentAssertions;
using FluentAssertions.Extensions;
using NUnit.Framework;
using static CleanArchitecture.Application.IntegrationTests.Testing;

namespace CleanArchitecture.Application.IntegrationTests.Cities.Commands
{
    public class CreateCityTests : TestBase
    {
        [Test]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCityCommand("");

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();

        }

        [Test]
        public async Task ShouldRequireUniqueName()
        {
            await RunAsDefaultUserAsync();

            await SendAsync(new CreateCityCommand( "Bursa" ));

            var command = new CreateCityCommand( "Bursa" );

            await FluentActions.Invoking(() =>
                SendAsync(command)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task ShouldCreateCity()
        {
            var userId = await RunAsDefaultUserAsync();

            var command = new CreateCityCommand( "Kastamonu" );

            var result = await SendAsync(command);

            var list = await FindAsync<City>(result.Data.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
            list.Creator.Should().Be(userId);
            list.CreateDate.Should().BeCloseTo(DateTime.Now, 10.Seconds());
        }
    }
}
