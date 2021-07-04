using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public interface ISortingGridView
    {
        ListSortDirection LastDirection { get; set; }
        GridViewColumnHeader LastHeaderClicked { get; set; }
    }

    public static class SortingGridView
    {
        public static void GridViewColumnHeaderClicked(this ISortingGridView obj, object sender, RoutedEventArgs e)
        {
            if (!(e.OriginalSource is GridViewColumnHeader headerClicked)) return;
            var lv = e.Source as ListView;

            if (headerClicked.Role == GridViewColumnHeaderRole.Padding) return;

            ListSortDirection direction;

            if (!Equals(headerClicked, obj.LastHeaderClicked))
            {
                direction = ListSortDirection.Ascending;
            }
            else
            {
                direction = obj.LastDirection == ListSortDirection.Ascending
                                ? ListSortDirection.Descending
                                : ListSortDirection.Ascending;
            }

            string bindingToSort;
            if (headerClicked.Column.DisplayMemberBinding is Binding dmb)
            {
                bindingToSort = dmb.Path.Path;
            }
            else
            {
                bindingToSort = headerClicked.Tag as string;
            }

            var success = Sort(lv, bindingToSort, direction);
            if (!success) return;

            // Add arrow to header
            if (direction == ListSortDirection.Ascending)
            {
                headerClicked.Column.HeaderTemplate =
                    ((FrameworkElement) obj).Resources["HeaderTemplateArrowUp"] as DataTemplate;
            }
            else
            {
                headerClicked.Column.HeaderTemplate =
                    ((FrameworkElement) obj).Resources["HeaderTemplateArrowDown"] as DataTemplate;
            }

            // Remove arrow from previously sorted header 
            if (obj.LastHeaderClicked != null && !Equals(obj.LastHeaderClicked, headerClicked))
            {
                obj.LastHeaderClicked.Column.HeaderTemplate = null;
            }

            obj.LastHeaderClicked = headerClicked;
            obj.LastDirection = direction;
        }

        public static void Thumb_DragDelta(this ISortingGridView obj, object sender, DragDeltaEventArgs e)
        {
            if (!(e.OriginalSource is Thumb senderAsThumb)) return;
            
            if (senderAsThumb.TemplatedParent is GridViewColumnHeader header) 
                header.Column.Width = double.NaN;
        }
        
        private static bool Sort(ItemsControl lv, string sortBy, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(sortBy)) return false;
            var dataView = CollectionViewSource.GetDefaultView(lv.ItemsSource);
            if (dataView == null) return false;

            dataView.SortDescriptions.Clear();
            var sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
            return true;
        }
    }
}