using System.Windows.Threading;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Interfaces
{
    /// <summary>
    ///     Provides a generalized mechanism for exporting a
    ///     DispatcherTimer and its events as an extension.
    /// </summary>
    public interface ITimerExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the DispatcherTimer.
        /// </summary>
        DispatcherTimer Timer(Logger logger);
    }
}