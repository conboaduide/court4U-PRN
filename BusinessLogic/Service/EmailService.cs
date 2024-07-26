using BusinessLogic.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class EmailService : IEmailService
    {
        public async Task SendEmail(string email, string token)
        {
            try
            {
                string link = "https://court4u.ddns.net/verify?token=" + $"{token}";

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // or the port your email provider uses
                    Credentials = new NetworkCredential("anhvnvse172006@fpt.edu.vn", "ahea kegk wmtv ptrs"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anhvnvse172006@fpt.edu.vn"),
                    Subject = "Verify",
                    Body = "Click this link to verify: " + $"{link}",
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it or rethrow it)
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            }
        }

        public async Task SendQrCode(string email, string content)
        {
            try
            {

                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // or the port your email provider uses
                    Credentials = new NetworkCredential("anhvnvse172006@fpt.edu.vn", "ahea kegk wmtv ptrs"),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("anhvnvse172006@fpt.edu.vn"),
                    Subject = "Confirm booking from Court4u",
                    Body = content,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Handle the exception (e.g., log it or rethrow it)
                Console.WriteLine($"An error occurred while sending the email: {ex.Message}");
            }
        }
    }
}
