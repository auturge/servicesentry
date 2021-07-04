using System.Windows.Controls;
using ServiceSentry.Extensibility.Logging;

namespace ServiceSentry.Extensibility.Interfaces
{
    /// <summary>
    ///     Provides a generalized mechanism for exporting a
    ///     TabItem and its events as an extension.
    /// </summary>
    public interface ITabExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the UserControl.
        /// </summary>
        TabItem TabExtensionItem(Logger logger);
    }
}