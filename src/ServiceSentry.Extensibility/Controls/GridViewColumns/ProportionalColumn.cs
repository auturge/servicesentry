using System.Windows;
using System.Windows.Controls;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public sealed class ProportionalColumn : LayoutColumn
    {
        public static readonly DependencyProperty WidthProperty =
            DependencyProperty.RegisterAttached("Width", typeof (double), typeof (ProportionalColumn));

        private ProportionalColumn()
        {
        }

        public static double GetWidth(DependencyObject obj)
        {
            return (double) obj.GetValue(WidthProperty);
        }

        public static void SetWidth(DependencyObject obj, double width)
        {
            obj.SetValue(WidthProperty, width);
        }

        public static bool IsProportionalColumn(GridViewColumn column)
        {
            return column != null && HasPropertyValue(column, WidthProperty);
        }

        public static double? GetProportionalWidth(GridViewColumn column)
        {
            return GetColumnWidth(column, WidthProperty);
        }

        public static GridViewColumn ApplyWidth(GridViewColumn gridViewColumn, double width)
        {
            SetWidth(gridViewColumn, width);
            return gridViewColumn;
        }
    }
}