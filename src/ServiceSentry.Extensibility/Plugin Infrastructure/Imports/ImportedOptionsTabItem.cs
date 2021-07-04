using System;
using System.Windows.Controls;
using ServiceSentry.Extensibility.Extensions;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Imports
{
    public class ImportedOptionsTabItem : ImportedExtension
    {
        public TabItem OptionTabItem = new TabItem();

        public ImportedOptionsTabItem()
        {
        }

        public ImportedOptionsTabItem(Logger logger, OptionsTabExtension control)
        {
            ExtensionName = control.ExtensionName;
            CanExecute = control.CanExecute;
            OptionTabItem = control.OptionTabItem(logger);
            CommitOptions = control.CommitOptions;
            RefreshOptionSettings = control.RefreshOptionSettings;
        }

        public Action CommitOptions { get; set; }
        public Action RefreshOptionSettings { get; set; }
    }
}