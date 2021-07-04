using System;
using System.Windows;
using System.Windows.Controls;

namespace ServiceSentry.Extensibility.Controls
{
    public sealed class ColumnVisibility : LayoutColumn
    {
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.RegisterAttached("IsVisible", typeof(bool), typeof(ColumnVisibility),
                new UIPropertyMetadata(true, OnIsVisibleChanged));

        private ColumnVisibility()
        {
        }

        public static bool GetIsVisible(DependencyObject obj)
        {
            return (bool) obj.GetValue(IsVisibleProperty);
        }

        public static void SetIsVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsVisibleProperty, value);
        }

        public static bool IsVisibilityColumn(GridViewColumn column)
        {
            return column != null && HasPropertyValue(column, IsVisibleProperty);
        }

        private static void OnIsVisibleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var gc = sender as GridViewColumn;
            if (gc == null) return;

            if (GetIsVisible(gc) == false)
            {
                gc.Width = 0;
            }
            else
            {
                if (!(Math.Abs(gc.Width - 0) < double.Epsilon)) return;
                if (double.IsNaN(gc.Width))
                {
                    gc.Width = gc.ActualWidth;
                }

                gc.Width = double.NaN;
            }
        }
    }
}