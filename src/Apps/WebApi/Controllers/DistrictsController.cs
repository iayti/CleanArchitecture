namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Application.Cities.Commands.Create;
    using Application.Common.Models;
    using Application.Districts.Queries;
    using Application.Dto;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class DistrictsController: BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id)
        {
            var vm = await Mediator.Send(new ExportDistrictsQuery { CityId = id });

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResult<DistrictDto>>> Create(CreateDistrictCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}
