using System.Windows.Controls;
using ServiceSentry.Extensibility.Logging;

// ReSharper disable UnusedMemberInSuper.Global

namespace ServiceSentry.Extensibility.Interfaces
{
    public interface IContextMenuExtension : IExtensionClass
    {
        /// <summary>
        ///     Gets the UserControl.
        /// </summary>
        ContextMenu ContextMenu(Logger logger);
    }
}