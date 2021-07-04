using System.Windows.Controls;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Extensions
{
    public abstract class ContextMenuExtension : NotifyPropertyChanged, IContextMenuExtension
    {
        public abstract string ExtensionName { get; }
        public abstract ContextMenu ContextMenu(Logger logger);

        /// <summary>
        ///     Indicates whether the context menu should be loaded/imported or not.
        /// </summary>
        public virtual bool CanExecute => false;


        public virtual void OnImportsSatisfied()
        {
        }
    }
}