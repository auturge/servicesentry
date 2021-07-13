using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Common.Services;
using ServiceSentry.Extensibility.Extensions;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Common
{
    /// <summary>
    ///     A class to hold imported timer items.
    /// </summary>
    public class ImportedServicesList : ImportedExtension
    {
        public ImportedServicesList()
        {
        }

        public ImportedServicesList(Logger logger, IServiceListExtension control)
        {
            control.Configure(logger);
            ExtensionName = control.ExtensionName;
            CanExecute = control.CanExecute;
            Services = control.Services;
            OtherFiles = control.OtherFiles;
        }

        public List<Service> Services { get; } = new List<Service>();

        public List<ExternalFile> OtherFiles { get; } = new List<ExternalFile>();
    }
}