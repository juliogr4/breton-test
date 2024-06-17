namespace Breton.Application.Services.Email;
public interface IEmailService
{
    void SendEmail(
        string to, 
        string subject, 
        string url);
}