using System.Configuration.Install;

namespace ServiceSentry.Common.ServiceFramework
{
    internal abstract class ManagedInstaller
    {
        internal static ManagedInstaller Default => new ManagedInstallerImplementation();

        public abstract void InstallHelper(string[] args);

        private sealed class ManagedInstallerImplementation : ManagedInstaller
        {
            public override void InstallHelper(string[] args)
            {
                ManagedInstallerClass.InstallHelper(args);
            }
        }
    }
}