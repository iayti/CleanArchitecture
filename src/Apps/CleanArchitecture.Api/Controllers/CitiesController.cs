using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Cities.Commands.Create;
using CleanArchitecture.Application.Cities.Commands.Delete;
using CleanArchitecture.Application.Cities.Commands.Update;
using CleanArchitecture.Application.Cities.Queries.GetCities;
using CleanArchitecture.Application.Cities.Queries.GetCityById;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
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
        public async Task<ActionResult<ServiceResult<CityDto>>> GetCityById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetCityByIdQuery { CityId = id }, cancellationToken));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<CityDto>>> Create(CreateCityCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<CityDto>>> Update(UpdateCityCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<CityDto>>> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteCityCommand { Id = id }, cancellationToken));
        }
    }
}