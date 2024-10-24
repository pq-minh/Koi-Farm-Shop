using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiShop.Application.Users
{
    public interface IEmailSender 
    {
        Task SendEmailAsync(string toEmail, string subject, string plainTextContent, string htmlContent);
     }
}
