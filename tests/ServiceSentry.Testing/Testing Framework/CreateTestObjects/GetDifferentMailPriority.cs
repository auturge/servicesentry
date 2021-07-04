using System;
using System.Net.Mail;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static MailPriority GetDifferentMailPriority(MailPriority current)
        {
            var array = Enum.GetValues(typeof (MailPriority));

            var num = (int) current;
            var newNum = (num + 1)%array.Length;

            return (MailPriority) array.GetValue(newNum);
        }
    }
}