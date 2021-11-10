using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Districts.Commands.Create;
using CleanArchitecture.Application.Districts.Queries;
using CleanArchitecture.Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Api.Controllers
{
    /// <summary>
    /// District
    /// </summary>
    [Authorize]
    public class DistrictsController: BaseApiController
    {
        /// <summary>
        /// Get district by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<FileResult> Get(int id, CancellationToken cancellationToken)
        {
            var vm = await Mediator.Send(new ExportDistrictsQuery { CityId = id }, cancellationToken);

            return File(vm.Content, vm.ContentType, vm.FileName);
        }

        /// <summary>
        /// Create district
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ServiceResult<DistrictDto>>> Create(CreateDistrictCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }
    }
}
