using System.Net;
using System.Net.Mail;

namespace recipe_tracker.Helpers;

public class EmailSender(
    string host,
    string from,
    string password,
    int port,
    string to,
    string message,
    string subject)
{
    public bool SendEmail()
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(from),
            Subject = subject,
            Body = message,
            IsBodyHtml = true
        };
        mailMessage.To.Add(new MailAddress(to));

        var networkCredential = new NetworkCredential
        {
            UserName = mailMessage.From.Address,
            Password = password
        };

        var smtp = new SmtpClient
        {
            Host = host,
            EnableSsl = true,
            Port = port,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = networkCredential
        };

        smtp.Send(mailMessage);
        return true;
    }
}