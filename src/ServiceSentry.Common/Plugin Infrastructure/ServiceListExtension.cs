using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Common.Services;
using ServiceSentry.Extensibility;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Common
{
    public abstract class ServiceListExtension : NotifyPropertyChanged, IServiceListExtension
    {
        public abstract string ExtensionName { get; }

        /// <summary>
        ///     Indicates whether the services should be loaded/imported or not.
        /// </summary>
        public abstract bool CanExecute { get; }

        public virtual void OnImportsSatisfied()
        {
        }

        public abstract List<Service> Services { get; }
        public abstract List<ExternalFile> OtherFiles { get; }
        public abstract void Configure(Logger logger);
    }
}