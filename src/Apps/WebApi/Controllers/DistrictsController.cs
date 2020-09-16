namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    using Application.Districts.Queries;
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
    }
}
