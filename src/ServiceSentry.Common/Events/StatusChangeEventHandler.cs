using System;
using ServiceSentry.Common.Enumerations;

namespace ServiceSentry.Common.Events
{
    // A delegate type for hooking up change notifications.
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);


    public class StatusChangedEventArgs : EventArgs
    {
        public ServiceState OldStatus { get; set; }
        public ServiceState NewStatus { get; set; }
    }
}