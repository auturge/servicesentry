using System.Windows.Threading;
using ServiceSentry.Extensibility.Extensions;
using ServiceSentry.Extensibility.Logging;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Imports
{
    /// <summary>
    ///     A class to hold imported timer items.
    /// </summary>
    public class ImportedTimerItem : ImportedExtension
    {
        public DispatcherTimer Timer = new DispatcherTimer();

        public ImportedTimerItem()
        {
        }

        public ImportedTimerItem(Logger logger, TimerExtension control)
        {
            //Contract.Requires(control != null);

            ExtensionName = control.ExtensionName;
            Timer = control.Timer(logger);
            CanExecute = control.CanExecute;
        }

        public void Start()
        {
            Timer.Start();
        }
    }
}