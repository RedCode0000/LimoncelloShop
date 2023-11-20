using LimoncelloShop.Domain.Interfaces;
using System.Net;
using System.Net.Mail;

namespace LimoncelloShop.Business.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _emailAddress = "ITVitaeRepairShop@gmail.com";
        private readonly string _password = "lawnmaknkxdezlbx";
        public async Task SendValidationEmailAsync(string email, string validationLink)
        {
            var message = new MailMessage();
            message.From = new MailAddress(_emailAddress);
            message.To.Add(email);

            message.Subject = "Lemonbros Accountactivatie";
            message.Body = $"Druk alstublieft op deze link om uw account te valideren: {validationLink}";

            SmtpClient client = new("smtp.gmail.com", 587);
            client.Credentials = new NetworkCredential(_emailAddress, _password);
            client.EnableSsl = true;

            await client.SendMailAsync(message);
        }
    }
}
