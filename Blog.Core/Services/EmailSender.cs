using System.Net;
using System.Net.Mail;

namespace Blog.Core.Services;

public class EmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mail = "hoangtuananh1772003@gmail.com";
        var pw = "vligdsqcyeaqlcex";

        var client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(mail, pw),
            EnableSsl = true
        };

        var message = new MailMessage(mail, email)
        {
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        return client.SendMailAsync(message);
    }
}