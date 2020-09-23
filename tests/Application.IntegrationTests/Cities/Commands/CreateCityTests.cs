namespace Application.IntegrationTests.Cities.Commands
{
    using System;
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Common.Exceptions;
    using Domain.Entities;
    using FluentAssertions;
    using Xunit;

    //using static Testing;

    public class CreateCityTests : IClassFixture<Testing>
    {
        public Testing _testing;

        public CreateCityTests(Testing testing)
        {
            _testing = testing;
        }

        [Fact]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCityCommand();

            FluentActions.Invoking(() =>
                _testing.SendAsync(command)).Should().Throw<ValidationException>();

        }

        [Fact]
        public async Task ShouldRequireUniqueName()
        {
            await _testing.SendAsync(new CreateCityCommand
            {
                Name = "Bursa"
            });

            var command = new CreateCityCommand
            {
                Name = "Bursa"
            };

            FluentActions.Invoking(() =>
                _testing.SendAsync(command)).Should().Throw<ValidationException>();
        }

        [Fact]
        public async Task ShouldCreateCity()
        {
            var userId = await _testing.RunAsDefaultUserAsync();

            var command = new CreateCityCommand
            {
                Name = "Kastamonu"
            };

            var result = await _testing.SendAsync(command);

            var list = await _testing.FindAsync<City>(result.Data.Id);

            list.Should().NotBeNull();
            list.Name.Should().Be(command.Name);
            list.Creator.Should().Be(userId);
            list.CreateDate.Should().BeCloseTo(DateTime.Now, 10000);
        }
    }
}
