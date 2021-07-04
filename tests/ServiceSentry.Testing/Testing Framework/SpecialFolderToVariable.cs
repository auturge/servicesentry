using System;

// ReSharper disable StringLiteralTypo

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static string SpecialFolderToVariable(Environment.SpecialFolder input)
        {
            switch (input)
            {
                case Environment.SpecialFolder.ProgramFiles:
                    return "%PROGRAMFILES%";

                case Environment.SpecialFolder.UserProfile:
                    return "%USERPROFILE%";

                case Environment.SpecialFolder.LocalApplicationData:
                    return "%LOCALAPPDATA%";

                case Environment.SpecialFolder.Windows:
                    return "%WINDIR%";
            }
            return string.Empty;
        }
    }
}