namespace WebApi.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Application.Cities.Queries.GetCities;
    using Application.Cities.Queries.GetCityById;
    using Application.Common.Models;
    using Application.Dto;
    using Application.Cities.Commands.Create;
    using Application.Cities.Commands.Delete;

    //[Authorize]
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<CityDto>>>> GetAllCities()
        {
            return Ok(await Mediator.Send(new GetAllCitiesQuery()));
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

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //    return NoContent();
        //}
    }
}
