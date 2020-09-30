namespace Infrastructure.Services
{
    using System.Net.Mail;
    using System.Threading.Tasks;

    using Application.Common.Interfaces;
    using Application.Common.Models;
    using Microsoft.Extensions.Logging;

    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public async Task SendAsync(EmailRequest request)
        {
            var emailClient = new SmtpClient("localhost");

            var message = new MailMessage
            {
                From = new MailAddress(request.FromMail),
                Subject = request.Subject,
                Body = request.Body
            };

            foreach (string to in request.ToMail)
            {
                message.To.Add(new MailAddress(to));
            }

            //TODO:EmailService Exception will be added. 
            //TODO:EmailService if there was error, try at least three times. 
            try
            {
                await emailClient.SendMailAsync(message);
            }
            catch 
            {

            }

            _logger.LogWarning($"Sending email to {request.ToMail} from {request.FromMail} with subject {request.Subject}.");

        }
    }
}
