namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Application.Cities.Queries.GetCities;
    using Application.Cities.Queries.GetCityById;
    using Application.Common.Models;
    using Application.Dto;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Delete;
    using Application.Cities.Commands.Update;

    [Authorize]
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<CityDto>>>> GetAllCities(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllCitiesQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<CityDto>>> GetCityById(int id)
        {
            return Ok(await Mediator.Send(new GetCityByIdQuery { CityId = id }));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<CityDto>>> Create(CreateCityCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<CityDto>>> Update(UpdateCityCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<CityDto>>> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCityCommand { Id = id }));
        }
    }
}
