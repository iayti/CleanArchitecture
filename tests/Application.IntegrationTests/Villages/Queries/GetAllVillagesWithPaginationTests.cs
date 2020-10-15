namespace Application.IntegrationTests.Villages.Queries
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Application.Villages.Queries.GetVillagesWithPagination;
    using Domain.Entities;
    using FluentAssertions;
    using NUnit.Framework;
    using static Testing;

    public class GetAllVillagesWithPaginationTests : TestBase
    {
        [Test]
        public async Task ShouldReturnAllCities()
        {
            await AddAsync(new City
            {
                Name = "Muğla",
            });

            await AddAsync(new District
            {
                Name = "Bodrum",
                CityId = 1
            });

            List<string> villages = new List<string> { "Çömlekçi", "Müsgebi", "Karakaya", "Etrim", "Sandima", "Akyarlar", "Gündoğan" };

            foreach (var name in villages)
            {
                await AddAsync(new Village
                {
                    Name = name,
                    DistrictId = 1
                });
            }

            var query = new GetAllVillagesWithPaginationQuery
            {
                DistrictId = 1,
                PageNumber = 0,
                PageSize = 3
            };

            var result = await SendAsync(query);

            result.Should().NotBeNull();
            result.Succeeded.Should().BeTrue();
            result.Data.TotalCount.Should().Be(3);
        }
    }
}
