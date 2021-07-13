using System.ServiceProcess;
using ServiceSentry.Common.Communication;

namespace ServiceSentry.Common.Services
{
    public abstract class LocalServiceFinder
    {
        public static LocalServiceFinder Default => new LocalServiceFinderImplementation();

        public abstract SubscriptionPacket[] GetServices();
        public abstract bool IsInstalled(string serviceName);


        private sealed class LocalServiceFinderImplementation : LocalServiceFinder
        {
            public override SubscriptionPacket[] GetServices()
            {
                var services = ServiceController.GetServices();

                var output = new SubscriptionPacket[services.Length];
                for (var i = 0; i < services.Length; i++)
                {
                    output[i] = new SubscriptionPacket
                        {
                            ServiceName = services[i].ServiceName,
                            DisplayName = services[i].DisplayName,
                            MachineName = services[i].MachineName
                        };
                }
                return output;
            }

            public override bool IsInstalled(string serviceName)
            {
                var output = GetServices();
                foreach (var item in output)
                {
                    if (item.ServiceName == serviceName) return true;
                }
                return false;
            }
        }
    }
}