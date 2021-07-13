using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Extensibility;

namespace ServiceSentry.Common
{
    public abstract class FileListExtension : NotifyPropertyChanged, IFileListExtension
    {
        public abstract string ExtensionName { get; }

        /// <summary>
        ///     Indicates whether the services should be loaded/imported or not.
        /// </summary>
        public abstract bool CanExecute { get; }

        public virtual void OnImportsSatisfied()
        {
        }

        public abstract List<ExternalFile> Files { get; set; }
    }
}