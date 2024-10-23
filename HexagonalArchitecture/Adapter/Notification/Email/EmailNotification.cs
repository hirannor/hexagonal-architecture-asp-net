using HexagonalArchitecture.Application.Port;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HexagonalArchitecture.Adapter.Notification.Email;

internal class EmailNotification(ILogger<EmailNotification> logger, IOptions<EmailSettings> settings)
    : IEmailNotification, IDisposable
{
    private readonly EmailSettings _emailSettings = settings.Value;
    private readonly SmtpClient _smtpClient = new();

    private MimeMessage CreateMimeMessage(SendEmailNotification cmd)
    {
        MimeMessage message = new MimeMessage();

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

    public async Task Send(SendEmailNotification cmd)
    {
        logger.LogDebug("Email notification adapter triggered...");

        MimeMessage message = CreateMimeMessage(cmd);

        try
        {
            logger.LogDebug("Establishing connection for SMTP server {server}:{port}",
                _emailSettings.SmtpServer, _emailSettings.Port);

            await _smtpClient.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.Port,
                SecureSocketOptions.StartTls
            );

            logger.LogDebug("Connection established successfully to SMTP server.");

            logger.LogDebug("Attempting to authenticate to server....");
            await _smtpClient.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            logger.LogDebug("Authentication to SMTP server was successful.");

            logger.LogDebug("Attempting to send email to {recipient}....", cmd.EmailAddress);
            await _smtpClient.SendAsync(message);
            logger.LogDebug("Email sent successfully.");
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
}