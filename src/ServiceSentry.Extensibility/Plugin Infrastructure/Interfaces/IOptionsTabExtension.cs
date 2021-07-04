using System.Windows.Controls;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Interfaces
{
    /// <summary>
    ///     Provides a generalized mechanism for exporting an
    ///     Options window TabItem and its events as an extension.
    /// </summary>
    public interface IOptionsTabExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the UserControl.
        /// </summary>
        TabItem OptionTabItem(Logger logger);

        void CommitOptions();

        void RefreshOptionSettings();
    }
}