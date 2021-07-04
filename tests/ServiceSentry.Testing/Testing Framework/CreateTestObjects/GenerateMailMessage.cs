using System.Net.Mail;
using System.Text;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static MailMessage GenerateMailMessage()
        {
            var message = new MailMessage
                {
                    From = new MailAddress("NoReply@ServiceSentry.test.org", "Service Sentry", Encoding.UTF8),
                    Sender = new MailAddress("curtis.kaler@test.org", "Service Sentry", Encoding.UTF8),
                    Subject = "Test Email",
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = Random<bool>(),
                    Priority = MailPriority.High,
                };
            message.To.Add(new MailAddress("curtis.kaler@test.org"));

            return message;
        }
    }
}