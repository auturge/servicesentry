using System.Collections.ObjectModel;
using ServiceSentry.Common.Enumerations;
using ServiceSentry.Common.Services;

namespace ServiceSentry.Common.Events
{
    public class ServiceWorkEventArgs
    {
        public ObservableCollection<Service> Collection { get; set; }
        public ServiceAction Action { get; set; }
    }
}
