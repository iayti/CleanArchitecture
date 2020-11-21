using Application.Cities.Commands.Create;
using Application.Cities.Commands.Delete;
using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Cities.Commands
{
    public class DeleteCityTests : TestBase
    {
        [Test]
        public void ShouldRequireValidCityId()
        {
            var command = new DeleteCityCommand { Id = 99 };

            FluentActions.Invoking(() =>
                SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task ShouldDeleteCity()
        {
            var city = await SendAsync(new CreateCityCommand
            {
                Name = "Kayseri"
            });

            await SendAsync(new DeleteCityCommand
            {
                Id = city.Data.Id
            });

            var list = await FindAsync<City>(city.Data.Id);

            list.Should().BeNull();
        }
    }
}
