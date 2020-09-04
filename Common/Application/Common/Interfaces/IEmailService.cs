namespace Application.Common.Interfaces
{
    using System.Threading.Tasks;

    using Models;

    public interface IEmailService
    {
        Task<Result> SendAsync(EmailRequest request);
    }
}
