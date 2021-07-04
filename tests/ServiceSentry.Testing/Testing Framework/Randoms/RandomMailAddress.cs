using System.Net.Mail;
using System.Text;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static MailAddress RandomMailAddress()
        {
            //var array = Enum.GetValues(typeof (Encoding));
            //var random = new Random((int) DateTime.Now.Ticks);
            //var encoding = (Encoding) array.GetValue(random.Next(array.Length));


            return new MailAddress(RandomAddress(), Random<string>(), Encoding.UTF8);
        }
    }
}