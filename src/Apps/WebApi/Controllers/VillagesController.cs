namespace WebApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Application.Common.Models;
    using Application.Dto;
    using Application.Villages.Queries.GetVillagesWithPagination;
    
    [Authorize]
    public class VillagesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<ServiceResult<PaginatedList<VillageDto>>>> GetAllVillagesWithPagination(GetAllVillagesWithPaginationQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
