using CleanArchitecture.Application.Dto;

namespace CleanArchitecture.Application.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
