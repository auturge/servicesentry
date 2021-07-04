using System.Windows.Controls;
using ServiceSentry.Extensibility.Extensions;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Imports
{
    /// <summary>
    ///     A class to hold imported timer items.
    /// </summary>
    public class ImportedContextMenu : ImportedExtension
    {
        public ContextMenu Menu = new ContextMenu();

        public ImportedContextMenu()
        {
        }

        public ImportedContextMenu(Logger logger, IContextMenuExtension control)
        {
            ExtensionName = control.ExtensionName;
            Menu = control.ContextMenu(logger);
            CanExecute = control.CanExecute;
        }
    }
}