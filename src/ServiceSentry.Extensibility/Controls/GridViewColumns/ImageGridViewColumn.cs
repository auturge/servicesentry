using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public abstract class ImageGridViewColumn : GridViewColumn, IValueConverter
    {
        protected ImageGridViewColumn() : this(Stretch.None)
        {
        }

        protected ImageGridViewColumn(Stretch imageStretch)
        {
            var imageElement = new FrameworkElementFactory(typeof (Image));

            var imageSourceBinding = new Binding {Converter = this, Mode = BindingMode.OneWay};
            imageElement.SetBinding(Image.SourceProperty, imageSourceBinding);

            var imageStretchBinding = new Binding {Source = imageStretch};
            imageElement.SetBinding(Image.StretchProperty, imageStretchBinding);

            var template = new DataTemplate {VisualTree = imageElement};
            CellTemplate = template;
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetImageSource(value);
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        protected abstract ImageSource GetImageSource(object value);
    }
}