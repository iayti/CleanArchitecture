namespace WebApi.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;

    using Application.Common.Models;
    using Application.ApplicationUser.Queries.GetToken;

    public class LoginController : BaseApiController
    {
        [HttpPost]
        public async Task<ActionResult<ServiceResult<LoginResponse>>> Create(GetTokenQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}
