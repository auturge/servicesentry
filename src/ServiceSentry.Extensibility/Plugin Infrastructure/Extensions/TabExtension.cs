using System.Windows.Controls;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Extensions
{
    public abstract class TabExtension : NotifyPropertyChanged, ITabExtension
    {
        /// <summary>
        ///     A boolean value that determines whether the extension will be loaded.
        /// </summary>
        public virtual bool CanExecute
        {
            get { return true; }
        }

        /// <summary>
        ///     Gets the name of this imported extension.
        /// </summary>
        public abstract string ExtensionName { get; }

        /// <summary>
        ///     Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public virtual void OnImportsSatisfied()
        {
        }

        /// <summary>
        ///     Gets the tab within this extension.
        /// </summary>
        public abstract TabItem TabExtensionItem(Logger logger);
    }
}