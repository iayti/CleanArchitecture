namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Application.Cities.Queries.GetCities;

    //[Authorize]
    public class CitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<GetAllCitiesResponse>> GetAllCities()
        {
            return await Mediator.Send(new GetAllCitiesQuery());
        }

        //[HttpGet("{id}")]
        //public async Task<FileResult> Get(int id)
        //{
        //    var vm = await Mediator.Send(new ExportTodosQuery { ListId = id });

        //    return File(vm.Content, vm.ContentType, vm.FileName);
        //}

        //[HttpPost]
        //public async Task<ActionResult<int>> Create(CreateTodoListCommand command)
        //{
        //    return await Mediator.Send(command);
        //}

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
