using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public abstract class ConverterGridViewColumn : GridViewColumn, IValueConverter
    {
        protected ConverterGridViewColumn(Type bindingType)
        {
            BindingType = bindingType ?? throw new ArgumentNullException(nameof(bindingType));
            DisplayMemberBinding = new Binding { Mode = BindingMode.OneWay, Converter = this };
        }

        public Type BindingType { get; }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!BindingType.IsInstanceOfType(value))
            {
                throw new InvalidOperationException();
            }
            return Convert(value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!BindingType.IsInstanceOfType(value))
            {
                throw new InvalidOperationException();
            }
            return ConvertBack(value);
        }

        protected abstract object Convert(object value);

        protected abstract object ConvertBack(object value);
    }
}