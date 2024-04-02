using MimeKit;
using MimeKit.Text;
using Support.Microservice.Contracts;
using Support.Microservice.Interfaces.Repositories;
using Support.Microservice.Interfaces.Services;
using Support.Microservice.Interfaces.Settings;

namespace Support.Microservice.Services;

public sealed class EmailService(ISupportEngineerRepository supportEngineerRepository,
                                 IEmailSender emailSender, 
                                 IConfiguration configuration) 
                                 : IEmailService
{
    public async Task SendTicketCreatedEmailAsync(TicketCreatedEvent ticketCreated)
    {
        var toEmailList = await supportEngineerRepository.GetAllEmailsEnabledAsync();

        if (!toEmailList.Any())
            return;

        var mailMessage = new MimeMessage()
        {
            Subject = "A ticket was created!",
            Body = new TextPart(TextFormat.Text)
            {
                Text = $"A ticket with Number: {ticketCreated.Number} and Id: {ticketCreated.Id} was created!"
            },
            From = { MailboxAddress.Parse(configuration["EmailCredentials:From"]) }
        };

        mailMessage.To.AddRange(toEmailList.Select(t => MailboxAddress.Parse(t)));

        await emailSender.SendEmailAsync(mailMessage);
    }
}
