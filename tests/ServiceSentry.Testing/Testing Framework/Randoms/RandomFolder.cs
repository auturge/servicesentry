using System;
using System.IO;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static string RandomFolder()
        {
            var root = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            var subDirs = root.GetDirectories();

            var directory = Randomizer.Next(subDirs.Length);
            var randomDirectory = subDirs[directory];

            return randomDirectory.FullName;
        }
    }
}