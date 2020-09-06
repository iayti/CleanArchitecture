namespace Application.IntegrationTests.Cities.Commands
{
    using System;
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Common.Exceptions;
    using Domain.Entities;
    using FluentAssertions;
    using Xunit;

    public class CreateCityTests : IClassFixture<TestBase>
    {
        private readonly TestBase _fixture;

        public CreateCityTests(TestBase fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ShouldRequireMinimumFields()
        {
            var command = new CreateCityCommand();

            FluentActions.Invoking(() =>
                _fixture.SendAsync(command)).Should().Throw<ValidationException>();

        }

        //[Fact]
        //public async Task ShouldRequireUniqueName()
        //{
        //    await _fixture.SendAsync(new CreateCityCommand
        //    {
        //        Name = "Bursa"
        //    });

        //    var command = new CreateCityCommand
        //    {
        //        Name = "Bursa"
        //    };

        //    FluentActions.Invoking(() =>
        //        _fixture.SendAsync(command)).Should().Throw<ValidationException>();
        //}

        //[Fact]
        //public async Task ShouldCreateCity()
        //{
        //    var userId = await _fixture.RunAsDefaultUserAsync();

        //    var command = new CreateCityCommand
        //    {
        //        Name = "İzmir"
        //    };

        //    var id = await _fixture.SendAsync(command);

        //    var list = await _fixture.FindAsync<City>(id);

        //    list.Should().NotBeNull();
        //    list.Name.Should().Be(command.Name);
        //    list.Creator.Should().Be(userId);
        //    list.CreateDate.Should().BeCloseTo(DateTime.Now, 10000);
        //}
    }
}
