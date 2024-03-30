using Azure.Core;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Fiorello.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void SendEmail(string email,string link,string emailTitle,string subject, string body)
        {
         
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress("7fvqmgj@code.edu.az", emailTitle);
            mailMessage.To.Add(new MailAddress(email));
            mailMessage.Subject = subject;
            mailMessage.Body = body;
         
               
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;

            smtpClient.Credentials = new NetworkCredential("7fvqmgj@code.edu.az", "nahq rjvx xbbc jaqo");
            smtpClient.Send(mailMessage);
        }
    }
}
