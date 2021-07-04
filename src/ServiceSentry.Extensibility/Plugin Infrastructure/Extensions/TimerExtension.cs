using System.Diagnostics;
using System.Windows.Threading;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Extensions
{
    public abstract class TimerExtension : NotifyPropertyChanged, ITimerExtension
    {
        /// <summary>
        ///     Gets the name of this imported extension.
        /// </summary>
        public abstract string ExtensionName { get; }

        /// <summary>
        ///     A boolean value that determines whether the extension will be loaded.
        /// </summary>
        public virtual bool CanExecute => false;

        /// <summary>
        ///     Called when a part's imports have been satisfied and it is safe to use.
        /// </summary>
        public virtual void OnImportsSatisfied()
        {
            Debug.WriteLine("  " + ExtensionName + ": successfully exported.");
        }

        /// <summary>
        ///     Gets the DispatcherTimer in the extension.
        /// </summary>
        public abstract DispatcherTimer Timer(Logger logger);
    }
}