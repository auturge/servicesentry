using System;

namespace ServiceSentry.Common.Events
{
    public sealed class FileChangedEventArgs : EventArgs
    {
        public string ServiceName { get; set; }
        public string LogFileName { get; set; }
        public string LogFilePath { get; set; }
    }
}