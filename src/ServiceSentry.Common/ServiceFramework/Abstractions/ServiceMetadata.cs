namespace ServiceSentry.Common.ServiceFramework
{
    internal sealed class ServiceMetadata
    {
        internal string EventLogName { get; set; }
        internal string EventLogSource { get; set; }
        internal WindowsService Implementation { get; set; }
        internal string LongDescription { get; set; }
        internal bool Quiet { get; set; }
        internal string ServiceName { get; set; }
        internal string ShortDescription { get; set; }
        internal bool Silent { get; set; }
    }
}