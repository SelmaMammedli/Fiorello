using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string email,string link, string emailTitle, string subject,string body);
    }
}
