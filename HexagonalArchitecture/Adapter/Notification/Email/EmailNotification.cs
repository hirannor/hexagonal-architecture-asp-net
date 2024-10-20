using HexagonalArchitecture.Application.Port;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace HexagonalArchitecture.Adapter.Notification.Email;

public class EmailNotification(ILogger<EmailNotification> logger, IOptions<EmailSettings> settings)
    : IEmailNotification, IDisposable
{
    private const string SendEmailNotificationCmdIsNull = "SendEmailNotification command should be not null!";

    private readonly EmailSettings _emailSettings = settings.Value;
    private readonly SmtpClient _smtpClient = new();

    public async Task Send(SendEmailNotification cmd)
    {
        if (cmd == null)
        {
            logger.LogError(SendEmailNotificationCmdIsNull);
            ArgumentNullException.ThrowIfNull(SendEmailNotificationCmdIsNull);
        }

        logger.LogDebug("Email notification adapter triggered...");
        
        var message = CreateMimeMessage(cmd);

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
            logger.LogDebug("authentication to SMTP server was successful....");

            logger.LogDebug("Attempting to send email to {recipient}....", cmd.EmailAddress);
            await _smtpClient.SendAsync(message);
            logger.LogDebug("authentication to SMTP server was successful....");
            
            logger.LogDebug("Email sent was successful....");
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
