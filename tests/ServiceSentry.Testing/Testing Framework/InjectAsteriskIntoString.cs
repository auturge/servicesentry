using System;
using System.IO;
using NUnit.Framework;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static string InjectAsteriskIntoString(string filename)
        {
            var extension = Path.GetExtension(filename);
            var shortFilename = Path.GetFileNameWithoutExtension(filename);
            if (shortFilename == null) Assert.Fail();

            var randIndex = (new Random()).Next(shortFilename.Length);
            return shortFilename.Substring(0, randIndex) + "*" +
                   shortFilename.Substring(randIndex + 1) + extension;
        }
    }
}