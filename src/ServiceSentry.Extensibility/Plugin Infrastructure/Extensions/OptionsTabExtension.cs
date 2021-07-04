using System.Windows.Controls;
using ServiceSentry.Extensibility.Interfaces;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Extensions
{
    public abstract class OptionsTabExtension : NotifyPropertyChanged, IOptionsTabExtension
    {
        public abstract string ExtensionName { get; }
        
        public abstract bool CanExecute { get; }
        public abstract void CommitOptions();

        public virtual void RefreshOptionSettings()
        {
        }

        public virtual void OnImportsSatisfied()
        {
        }

        public abstract TabItem OptionTabItem(Logger logger);
    }
}