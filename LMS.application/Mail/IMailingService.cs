using System.Net.Mail;

namespace LMS.Application.Mail
{
    public interface IMailingService
    {
        void SendMail(MailMessage message);
    }
}
