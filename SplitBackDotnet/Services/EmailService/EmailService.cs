using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using SplitBackDotnet.Dtos;
using SplitBackDotnet.Models;

namespace SplitBackDotnet.Services.EmailService;

public class EmailService : IEmailService {
  private readonly EmailSettings _emailSettings;

  public EmailService(IOptions<EmailSettings> emailSettings) {
    _emailSettings = emailSettings.Value;
  }

  public void SendEmail(EmailDto request) {
    var email = new MimeMessage();
    email.From.Add(MailboxAddress.Parse(_emailSettings.Username));
    email.To.Add(MailboxAddress.Parse(request.To));
    email.Subject = request.Subject;
    email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

    using var smtp = new SmtpClient();
    smtp.Connect(_emailSettings.Host, 587, SecureSocketOptions.StartTls);
    smtp.Authenticate(_emailSettings.Username, _emailSettings.Password);
    smtp.Send(email);
    smtp.Disconnect(true);
  }
}

