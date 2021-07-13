using System.Collections.Generic;
using ServiceSentry.Common.Files;
using ServiceSentry.Extensibility.Extensions;

namespace ServiceSentry.Common
{
    /// <summary>
    ///     A class to hold imported timer items.
    /// </summary>
    public class ImportedFileList : ImportedExtension
    {
        public ImportedFileList()
        {
        }

        public ImportedFileList(FileListExtension control)
        {
            //Contract.Requires(control != null);

            ExtensionName = control.ExtensionName;
            CanExecute = control.CanExecute;
            Items = control.Files;
        }

        public List<ExternalFile> Items { get; set; }
    }
}