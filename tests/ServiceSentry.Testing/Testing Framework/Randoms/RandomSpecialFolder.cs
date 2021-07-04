using System;
using System.Collections.Generic;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static Environment.SpecialFolder RandomSpecialFolder()
        {
            var validFolders = new List<Environment.SpecialFolder>
                {
                    Environment.SpecialFolder.ProgramFiles,
                    Environment.SpecialFolder.UserProfile,
                    Environment.SpecialFolder.LocalApplicationData,
                    Environment.SpecialFolder.Windows,
                };

            return validFolders[Randomizer.Next(validFolders.Count)];
        }
    }
}