using System.Net;
using System.Net.Mail;
using Breton.Application.Models;
using Microsoft.Extensions.Options;

namespace Breton.Application.Services.Email;
public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;
    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public void SendEmail(string to, string subject, string url)
    {
        try
        {
            var mailMessage = new MailMessage
            {
                To = { new MailAddress(to) },
                Subject = subject,
                From = new MailAddress(_emailSettings.From),
                Body = url
            };

            SmtpClient smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port);

            smtp.Credentials = new NetworkCredential(_emailSettings.From, _emailSettings.Password);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;

            smtp.Send(mailMessage);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro ao enviar o email", ex);
        }
    }
}