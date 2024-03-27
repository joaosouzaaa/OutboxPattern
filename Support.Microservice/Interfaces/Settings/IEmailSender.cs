using MimeKit;

namespace Support.Microservice.Interfaces.Settings;

public interface IEmailSender
{
    Task SendEmailAsync(MimeMessage mailMessage);
}
