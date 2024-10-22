namespace HexagonalArchitecture.Adapter.Notification.Email;

public class EmailSettings(string username, string password, int port, string smtpServer, string fromEmail)
{
    public string Username { get; set; } = username;
    public string Password { get; set; } = password;
    public int Port { get; set; } = port;
    public string SmtpServer { get; set; } = smtpServer;
    public string FromEmail { get; set; } = fromEmail;
}