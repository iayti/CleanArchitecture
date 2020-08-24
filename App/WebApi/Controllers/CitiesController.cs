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
            return await Mediator.Send(command);
        }

        //[HttpPut("{id}")]
        //public async Task<ActionResult> Update(int id, UpdateTodoListCommand command)
        //{
        //    if (id != command.Id)
        //    {
        //        return BadRequest();
        //    }

        //    await Mediator.Send(command);

        //    return NoContent();
        //}

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> Delete(int id)
        //{
        //    await Mediator.Send(new DeleteTodoListCommand { Id = id });

        //    return NoContent();
        //}
    }
}
