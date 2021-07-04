using System.ComponentModel;
using System.Windows.Controls;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public class SortingGridViewTabItem : TabItem, ISortingGridView
    {
        public SortingGridViewTabItem()
        {
            LastDirection = ListSortDirection.Ascending;
        }

        public ListSortDirection LastDirection { get; set; }
        public GridViewColumnHeader LastHeaderClicked { get; set; }
    }
}