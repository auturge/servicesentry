using System.Collections.ObjectModel;

namespace ServiceSentry.Extensibility.Logging
{
    public abstract class LoggingUtilities
    {
        public static LoggingUtilities GetInstance()
        {
            return new LoggingUtilitiesImplementation();
        }

        private sealed class LoggingUtilitiesImplementation : LoggingUtilities
        {
            public ObservableCollection<string> LogLevelNames
            {
                get
                {
                    var output = new ObservableCollection<string>();
                    return output;
                }
            }
        }
    }
}