using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Common.Services;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Common
{
    /// <summary>
    ///     Provides a generalized mechanism for exporting an
    ///     <see cref="List{T}" /> as an extension.
    /// </summary>
    public interface IServiceListExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the Service List.
        /// </summary>
        List<Service> Services { get; }

        List<ExternalFile> OtherFiles { get; }

        void Configure(Logger logger);
    }
}