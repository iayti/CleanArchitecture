using System;
using System.Net.Mail;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Infrastructure.Services
{
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
 
            //TODO:EmailService if there was error, try at least three times. 
            try
            {
                await emailClient.SendMailAsync(message);
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "CleanArchitecture EmailService: Unhandled Exception for Request {@Request}", request);
            }

            _logger.LogWarning($"Sending email to {request.ToMail} from {request.FromMail} with subject {request.Subject}.");

        }
    }
}
