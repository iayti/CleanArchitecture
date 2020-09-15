namespace Application.ApplicationUser.Queries.GetToken
{
    using Dto;

    public class LoginResponse
    {
        public ApplicationUserDto User { get; set; }

        public string Token { get; set; }
    }
}
