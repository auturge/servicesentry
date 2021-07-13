using System;

namespace ServiceSentry.Common.Events
{
    /// <summary>
    ///     A delegate type for hooking up error notifications from the Monitor Service.
    ///     Defines the signature of methods used to handle the Monitor Error notification event.
    /// </summary>
    /// <param name="sender">The object which fires the event.</param>
    /// <param name="e">
    ///     Event data identifying the exceptions thrown,
    ///     as well as the machine and service from which the error came.
    /// </param>
    public delegate void MonitorErrorEventHandler(object sender, MonitorErrorEventArgs e);

    /// <summary>
    ///     Provides data for the Monitor Error notification event.
    /// </summary>
    public sealed class MonitorErrorEventArgs : EventArgs
    {
        /// <summary>
        ///     The <see cref="ServiceName" /> of the service being queried when the event was raised.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     The <see cref="MachineName" /> of the machine that raised the event.
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///     The exceptions being reported by the event.
        /// </summary>
        public Exception[] Exceptions { get; set; }
    }
}