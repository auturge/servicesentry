using System;
using System.IO;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static string RandomFilePath()
        {
            return Environment.CurrentDirectory + "\\" + Path.GetRandomFileName();
        }
    }
}