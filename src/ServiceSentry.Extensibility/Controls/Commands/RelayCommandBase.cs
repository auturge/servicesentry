using System;
using System.Diagnostics;
using System.Windows.Input;

namespace ServiceSentry.Extensibility.Controls
{
    public abstract class RelayCommandBase : ICommand
    {
        protected Predicate<object> ProtectedCanExecute;
        protected string ProtectedName;

        /// <summary>
        ///     Gets the name of the command.
        /// </summary>
        /// <returns>
        ///     The name of the command.
        /// </returns>
        public string Name => ProtectedName;

        public abstract void Execute(object parameter);

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return ProtectedCanExecute == null || ProtectedCanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}