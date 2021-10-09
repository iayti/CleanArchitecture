using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Application.Villages.Queries.GetVillagesWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    [Authorize]
    public class VillagesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<VillageDto>>>> GetAllVillagesWithPagination(GetAllVillagesWithPaginationQuery query, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(query, cancellationToken));
        }
    }
}
