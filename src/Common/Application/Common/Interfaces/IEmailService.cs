namespace Application.Common.Interfaces
{
    using System.Threading.Tasks;

    using Models;

    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
