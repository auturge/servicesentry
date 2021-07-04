using System.Collections.Generic;
using Microsoft.Win32;

namespace ServiceSentry.Testing
{
    public partial class Tests
    {
        public static RegistryKey RandomRegistryKey()
        {
            var list = new List<RegistryKey>
                {
                    Registry.ClassesRoot,
                    Registry.CurrentConfig,
                    Registry.CurrentUser,
                    Registry.LocalMachine,
                    Registry.PerformanceData,
                    Registry.Users
                };

            return list[Randomizer.Next(list.Count)];
        }
    }
}