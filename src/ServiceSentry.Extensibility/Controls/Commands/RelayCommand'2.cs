using System;
using System.Globalization;

namespace ServiceSentry.Extensibility.Controls
{
    public partial class RelayCommand
    {
        private readonly Action<object, object> _execute2;

        public RelayCommand(string name, Action<object, object> execute)
            : this(name, execute, null)
        {
        }

        public RelayCommand(string name, Action<object, object> execute, Predicate<object> canExecute)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (name.Length == 0)
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.EXCEPTION_CommandNameCannotBeEmpty), nameof(name));

            ProtectedName = name;
            _execute2 = execute ?? throw new ArgumentNullException(nameof(execute));
            ProtectedCanExecute = canExecute;
        }

        public void Execute(object parameter1, object parameter2)
        {
            _execute2(parameter1, parameter2);
        }
    }
}