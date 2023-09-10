using System.Net;
using System.Net.Mail;

using Commands.Configuration;

using Domain;

using Microsoft.Extensions.Options;

namespace Commands.Services;

public interface IMailingService
{
    Task<IEnumerable<SendError>> SendMailAsync(IEnumerable<Member> members, Message message, CancellationToken ct);
}

public class MailingService : IMailingService
{
    private readonly MailingConfiguration _config;
    public MailingService(IOptions<MailingConfiguration> options)
    {
        if (options is null)
            throw new ArgumentNullException(nameof(options));

        _config = options.Value;
    }

    public async Task<IEnumerable<SendError>> SendMailAsync(IEnumerable<Member> members, Message message, CancellationToken ct)
    {
        var sender = new MailAddress(_config.Sender.Email, _config.Sender.Name);
        var recipients = members.Select(m => new MailAddress(m.Email ?? throw new Exception("Can't send email to members without email address"), m.Name));

        var messages = recipients.Select(r => new MailMessage(sender, r)
        {
            Subject = message.Subject,
            Body = message.Body

        });

        var errors = new List<SendError>();

        var mailTasks = messages.Select(async msg =>
        {
            try
            {
                using var client = GetSmtpClient;
                await client.SendMailAsync(msg, ct);
            }
            catch (SmtpException ex)
            {
                errors.Add(new(msg.To.Single().Address, ex.Message));
            }
        });

        await Task.WhenAll(mailTasks);

        return errors;
    }

    private SmtpClient GetSmtpClient => new SmtpClient(_config.MailServer.Host, _config.MailServer.Port)
    {
        EnableSsl = _config.MailServer.EnableSsl,
        TargetName = _config.MailServer.Host == "smtp.office365.com"
            ? "STARTTLS/smtp.office365.com"
            : null,
        Credentials = _config.MailServer.HaveCredentials
            ? new NetworkCredential(_config.MailServer.Username, _config.MailServer.Password)
            : null
    };
}

public record Message(string Subject, string Body);
public record SendError(string Email, string Error);
