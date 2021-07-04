using System;

// ReSharper disable StringLiteralTypo

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static char RandomChar()
        {
            var chars = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&".ToCharArray();
            var r = new Random();
            var i = r.Next(chars.Length);
            return chars[i];
        }
    }
}