using System.Threading.Tasks;
using CleanArchitecture.Application.ApplicationUser.Queries.GetToken;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Api.Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
