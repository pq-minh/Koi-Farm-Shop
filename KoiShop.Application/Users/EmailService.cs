using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace KoiShop.Application.Users
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Tranduy", "tranduy256789@gmail.com"));
            message.To.Add(new MailboxAddress("Recipient Name", toEmail));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = htmlContent
            };

            using (var client = new SmtpClient())
            {
                // Kết nối đến SMTP server của Gmail
                await client.ConnectAsync("smtp.gmail.com", 587, false); // Thay đổi ở đây

                // Xác thực
                await client.AuthenticateAsync("tranduy256789@gmail.com", "ulfy vxdu rdfq sukx"); // Sử dụng mã ứng dụng nếu cần

                // Gửi email
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }
    }
}
