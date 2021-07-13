using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Extensibility.Interfaces;

namespace ServiceSentry.Common
{
    /// <summary>
    ///     Provides a generalized mechanism for exporting an
    ///     <see cref="List{T}" /> as an extension.
    /// </summary>
    public interface IFileListExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the LogFiles.
        /// </summary>
        List<ExternalFile> Files { get; }
    }
}