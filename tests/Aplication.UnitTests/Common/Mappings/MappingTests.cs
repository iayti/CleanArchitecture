namespace Application.UnitTests.Common.Mappings
{
    using System;

    using MapsterMapper;

    using Application.Dto;
    using Domain.Entities;
    using FluentAssertions;
    using Mapster;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;

    public class MappingTests
    {
        private readonly IMapper _mapper;

        public MappingTests()
        {
            TypeAdapterConfig typeAdapterConfig = new TypeAdapterConfig();

            IServiceCollection services = new ServiceCollection();
            services.AddSingleton(typeAdapterConfig);
            services.AddScoped<IMapper, ServiceMapper>();

            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            _mapper = scope.ServiceProvider.GetService<IMapper>();
        }


        [Test]
        [TestCase(typeof(City), typeof(CityDto))]
        [TestCase(typeof(District), typeof(DistrictDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);

            _mapper.Map(instance, source, destination);
        }

        [Test]
        public void ShouldMappingCorrectly()
        {
            var city = new City { Id = 1, Name = "Bursa" };
            var cityDto = _mapper.Map<City, CityDto>(city);
            cityDto.Name.Should().Be("Bursa");
        }
    }
}
