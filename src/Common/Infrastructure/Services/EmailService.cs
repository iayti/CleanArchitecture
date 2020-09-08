namespace Infrastructure.Services
{
    using System.Threading.Tasks;

    using Application.Common.Interfaces;
    using Application.Common.Models;

    public class EmailService :IEmailService
    {
        public Task<Result> SendAsync(EmailRequest request)
        {
            //TODO: Add EmailService
            throw new System.NotImplementedException();
        }
    }
}
