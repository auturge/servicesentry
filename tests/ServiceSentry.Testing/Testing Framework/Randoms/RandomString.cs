using System.Diagnostics;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        [DebuggerStepThrough]
        private static string RandomString()
        {
            const int fixedLength = 10;
            return RandomString(fixedLength);
        }

        [DebuggerStepThrough]
        private static string RandomString(int length)
        {
            var buf = new char[length];
            for (var i = 0; i < length; i++)
            {
                var index = Randomizer.Next(AlphaNumeric.Length);
                buf[i] = AlphaNumeric[index];
            }

            return new string(buf);
        }
    }
}