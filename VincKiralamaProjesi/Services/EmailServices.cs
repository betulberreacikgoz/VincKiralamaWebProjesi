using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace VincKiralamaProjesi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendFirmKeyAsync(string toEmail, string firmName, string firmKey)
        {
            var host = _config["Smtp:Host"];
            var port = int.Parse(_config["Smtp:Port"] ?? "587");
            var user = _config["Smtp:User"];
            var pass = _config["Smtp:Pass"];
            var fromName = _config["Smtp:FromName"];
            var fromEmail = _config["Smtp:FromEmail"];

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = "Firma Anahtarınız | Vinç Kiralama";

            message.Body = new TextPart("plain")
            {
                Text =
$@"Merhaba {firmName},

Başvurunuz onaylandı ✅

Firma Anahtarınız: {firmKey}

Giriş:
https://localhost:5001/FirmAuth/Login

İyi çalışmalar."
            };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(user, pass);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
    }
}
