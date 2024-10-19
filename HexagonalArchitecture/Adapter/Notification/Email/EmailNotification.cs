using HexagonalArchitecture.Application.Port;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HexagonalArchitecture.Adapter.Notification.Email;

public class EmailNotification(ILogger<EmailNotification> logger, IOptions<EmailSettings> settings)
    : IEmailNotification, IDisposable
{
    private readonly EmailSettings _emailSettings = settings.Value;
    private readonly SmtpClient _smtpClient = new();

    public async Task Send(SendEmailNotification cmd)
    {
        var message = CreateMimeMessage(cmd);

        try
        {
            await _smtpClient.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.Port,
                MailKit.Security.SecureSocketOptions.StartTls
            );

            await _smtpClient.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            await _smtpClient.SendAsync(message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to send email to {EmailAddress}", cmd.EmailAddress);
            throw;
        }
        finally
        {
            await _smtpClient.DisconnectAsync(true);
        }
    }

    private MimeMessage CreateMimeMessage(SendEmailNotification cmd)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress("noreply", _emailSettings.FromEmail));
        message.To.Add(new MailboxAddress("", cmd.EmailAddress));
        message.Subject = cmd.Subject;
        message.Body = new TextPart("html") { Text = cmd.Content };

        return message;
    }

    public void Dispose()
    {
        if (_smtpClient.IsConnected)
        {
            _smtpClient.Disconnect(true); 
        }
        _smtpClient.Dispose(); 
    }
}
