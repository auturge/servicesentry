using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

// ReSharper disable UnusedMember.Global

namespace ServiceSentry.Extensibility.Controls
{
    public class ListViewLayoutManager
    {
        private const double ZeroWidthRange = 0.1;

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof (bool),
            typeof (ListViewLayoutManager),
            new FrameworkPropertyMetadata(OnLayoutManagerEnabledChanged));

        private GridViewColumn _autoSizedColumn;
        private bool _loaded;
        private Cursor _resizeCursor;
        private bool _resizing;
        private ScrollViewer _scrollViewer;

        public ListViewLayoutManager(ListView listView)
        {
            ListView = listView ?? throw new ArgumentNullException(nameof(listView));
            ListView.Loaded += ListViewLoaded;
            ListView.Unloaded += ListViewUnloaded;
        }

        public ListView ListView { get; }

        public ScrollBarVisibility VerticalScrollBarVisibility { get; set; } = ScrollBarVisibility.Auto;

        public static void SetEnabled(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(EnabledProperty, enabled);
        }

        public void Refresh()
        {
            InitColumns();
            DoResizeColumns();
        }

        private void RegisterEvents(DependencyObject start)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                switch (childVisual)
                {
                    case Thumb thumb:
                    {
                        var gridViewColumn = FindParentColumn(childVisual);
                        if (gridViewColumn != null)
                        {
                            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                                FixedColumn.IsFixedColumn(gridViewColumn) || IsFillColumn(gridViewColumn))
                            {
                                thumb.IsHitTestVisible = false;
                            }
                            else
                            {
                                thumb.PreviewMouseMove += ThumbPreviewMouseMove;
                                thumb.PreviewMouseLeftButtonDown += ThumbPreviewMouseLeftButtonDown;
                                DependencyPropertyDescriptor.FromProperty(
                                    GridViewColumn.WidthProperty,
                                    typeof (GridViewColumn)).AddValueChanged(gridViewColumn, GridColumnWidthChanged);
                            }
                        }

                        break;
                    }
                    case GridViewColumnHeader columnHeader:
                        columnHeader.SizeChanged += GridColumnHeaderSizeChanged;
                        break;
                    default:
                    {
                        if (_scrollViewer == null && childVisual is ScrollViewer viewer)
                        {
                            _scrollViewer = viewer;
                            _scrollViewer.ScrollChanged += ScrollViewerScrollChanged;
                            _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                            _scrollViewer.VerticalScrollBarVisibility = VerticalScrollBarVisibility;
                        }

                        break;
                    }
                }

                RegisterEvents(childVisual); 
            }
        }

        private void UnregisterEvents(DependencyObject start)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                switch (childVisual)
                {
                    case Thumb visual:
                    {
                        var gridViewColumn = FindParentColumn(childVisual);
                        if (gridViewColumn != null)
                        {
                            var thumb = visual;
                            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                                FixedColumn.IsFixedColumn(gridViewColumn) || IsFillColumn(gridViewColumn))
                            {
                                thumb.IsHitTestVisible = true;
                            }
                            else
                            {
                                thumb.PreviewMouseMove -= ThumbPreviewMouseMove;
                                thumb.PreviewMouseLeftButtonDown -= ThumbPreviewMouseLeftButtonDown;
                                DependencyPropertyDescriptor.FromProperty(
                                    GridViewColumn.WidthProperty,
                                    typeof (GridViewColumn)).RemoveValueChanged(gridViewColumn, GridColumnWidthChanged);
                            }
                        }

                        break;
                    }
                    case GridViewColumnHeader header:
                    {
                        var columnHeader = header;
                        columnHeader.SizeChanged -= GridColumnHeaderSizeChanged;
                        break;
                    }
                    default:
                    {
                        if (_scrollViewer == null && childVisual is ScrollViewer viewer)
                        {
                            _scrollViewer = viewer;
                            _scrollViewer.ScrollChanged -= ScrollViewerScrollChanged;
                        }

                        break;
                    }
                }
                UnregisterEvents(childVisual);
            }
        }

        private GridViewColumn FindParentColumn(DependencyObject element)
        {
            if (element == null)
            {
                return null;
            }

            while (element != null)
            {
                if (element is GridViewColumnHeader gridViewColumnHeader)
                    return (gridViewColumnHeader).Column;
                
                element = VisualTreeHelper.GetParent(element);
            }

            return null;
        }
        
        private GridViewColumnHeader FindColumnHeader(DependencyObject start, GridViewColumn gridViewColumn)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(start); i++)
            {
                var childVisual = VisualTreeHelper.GetChild(start, i) as Visual;
                if (childVisual is GridViewColumnHeader gridViewHeader)
                {
                    if (gridViewHeader.Column == gridViewColumn)
                    {
                        return gridViewHeader;
                    }
                }
                var childGridViewHeader = FindColumnHeader(childVisual, gridViewColumn);
                if (childGridViewHeader != null)
                {
                    return childGridViewHeader;
                }
            }
            return null;
        }

        private void InitColumns()
        {
            if (!(ListView.View is GridView view))
            {
                return;
            }

            foreach (var gridViewColumn in view.Columns)
            {
                if (ColumnVisibility.IsVisibilityColumn(gridViewColumn))
                {

                }



                if (!RangeColumn.IsRangeColumn(gridViewColumn))
                {
                    continue;
                }

                var minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
                var maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);
                if (!minWidth.HasValue && !maxWidth.HasValue)
                {
                    continue;
                }

                var columnHeader = FindColumnHeader(ListView, gridViewColumn);
                if (columnHeader == null)
                {
                    continue;
                }

                var actualWidth = columnHeader.ActualWidth;
                if (minWidth.HasValue)
                {
                    columnHeader.MinWidth = minWidth.Value;
                    if (!double.IsInfinity(actualWidth) && actualWidth < columnHeader.MinWidth)
                    {
                        gridViewColumn.Width = columnHeader.MinWidth;
                    }
                }
                if (maxWidth.HasValue)
                {
                    columnHeader.MaxWidth = maxWidth.Value;
                    if (!double.IsInfinity(actualWidth) && actualWidth > columnHeader.MaxWidth)
                    {
                        gridViewColumn.Width = columnHeader.MaxWidth;
                    }
                }
            }
        }
        
        protected virtual void ResizeColumns()
        {
            if (!(ListView.View is GridView view) || view.Columns.Count == 0)
            {
                return;
            }
            
            var actualWidth = double.PositiveInfinity;
            if (_scrollViewer != null)
            {
                actualWidth = _scrollViewer.ViewportWidth;
            }
            if (double.IsInfinity(actualWidth))
            {
                actualWidth = ListView.ActualWidth;
            }
            if (double.IsInfinity(actualWidth) || actualWidth <= 0)
            {
                return;
            }

            double resizeableRegionCount = 0;
            double otherColumnsWidth = 0;
            
            foreach (var gridViewColumn in view.Columns)
            {
                if (ProportionalColumn.IsProportionalColumn(gridViewColumn))
                {
                    var proportionalWidth = ProportionalColumn.GetProportionalWidth(gridViewColumn);
                    if (proportionalWidth != null)
                    {
                        resizeableRegionCount += proportionalWidth.Value;
                    }
                }
                else
                {
                    otherColumnsWidth += gridViewColumn.ActualWidth;
                }
            }

            if (resizeableRegionCount <= 0)
            {
                if (_scrollViewer != null)
                    _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
                
                var fillColumn = view.Columns.FirstOrDefault(IsFillColumn);

                if (fillColumn == null) return;

                var otherColumnsWithoutFillWidth = otherColumnsWidth - fillColumn.ActualWidth;
                var fillWidth = actualWidth - otherColumnsWithoutFillWidth;
                
                if (!(fillWidth > 0)) return;
                var minWidth = RangeColumn.GetRangeMinWidth(fillColumn);
                var maxWidth = RangeColumn.GetRangeMaxWidth(fillColumn);

                var setWidth = !(fillWidth < minWidth);
                if (fillWidth > maxWidth)
                {
                    setWidth = false;
                }

                if (!setWidth) return;
                if (_scrollViewer != null)
                {
                    _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                }
                fillColumn.Width = fillWidth;
                return;
            }

            var resizeableColumnsWidth = actualWidth - otherColumnsWidth;
            if (resizeableColumnsWidth <= 0)
            {
                return; 
            }

            var resizeableRegionWidth = resizeableColumnsWidth/resizeableRegionCount;
            foreach (var gridViewColumn in view.Columns)
            {
                if (!ProportionalColumn.IsProportionalColumn(gridViewColumn)) continue;
                var proportionalWidth = ProportionalColumn.GetProportionalWidth(gridViewColumn);
                if (proportionalWidth != null)
                {
                    gridViewColumn.Width = proportionalWidth.Value*resizeableRegionWidth;
                }
            }
        }

        private double SetRangeColumnToBounds(GridViewColumn gridViewColumn)
        {
            var startWidth = gridViewColumn.Width;

            var minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
            var maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);

            if (minWidth > maxWidth) return 0; 
            
            if (minWidth.HasValue && gridViewColumn.Width < minWidth.Value)
            {
                gridViewColumn.Width = minWidth.Value;
            }
            else if (maxWidth.HasValue && gridViewColumn.Width > maxWidth.Value)
            {
                gridViewColumn.Width = maxWidth.Value;
            }

            return gridViewColumn.Width - startWidth;
        }

        private bool IsFillColumn(GridViewColumn gridViewColumn)
        {
            if (gridViewColumn == null) return false;
            if (!(ListView.View is GridView view) || view.Columns.Count == 0) return false;
            var isFillColumn = RangeColumn.GetRangeIsFillColumn(gridViewColumn);
            return isFillColumn.HasValue && isFillColumn.Value;
        }

        private void DoResizeColumns()
        {
            if (_resizing) return;
        
            _resizing = true;
            try
            {
                ResizeColumns();
            }
            finally
            {
                _resizing = false;
            }
        }
        
        private void ListViewLoaded(object sender, RoutedEventArgs e)
        {
            RegisterEvents(ListView);
            InitColumns();
            DoResizeColumns();
            _loaded = true;
        }

        private void ListViewUnloaded(object sender, RoutedEventArgs e)
        {
            if (!_loaded)
            {
                return;
            }
            UnregisterEvents(ListView);
            _loaded = false;
        }

        private void ThumbPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (!(sender is Thumb thumb)) return;
            var gridViewColumn = FindParentColumn(thumb);
            if (gridViewColumn == null)
            {
                return;
            }

            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                FixedColumn.IsFixedColumn(gridViewColumn) ||
                IsFillColumn(gridViewColumn))
            {
                thumb.Cursor = null;
                return;
            }

            if (thumb.IsMouseCaptured && RangeColumn.IsRangeColumn(gridViewColumn))
            {
                var minWidth = RangeColumn.GetRangeMinWidth(gridViewColumn);
                var maxWidth = RangeColumn.GetRangeMaxWidth(gridViewColumn);

                if (minWidth > maxWidth)
                {
                    return; 
                }

                if (_resizeCursor == null)
                {
                    _resizeCursor = thumb.Cursor; 
                }

                if (minWidth.HasValue && gridViewColumn.Width <= minWidth.Value)
                {
                    thumb.Cursor = Cursors.No;
                }
                else if (maxWidth.HasValue && gridViewColumn.Width >= maxWidth.Value)
                {
                    thumb.Cursor = Cursors.No;
                }
                else
                {
                    thumb.Cursor = _resizeCursor; 
                }
            }
        }

        private void ThumbPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var thumb = sender as Thumb;
            var gridViewColumn = FindParentColumn(thumb);

            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) ||
                FixedColumn.IsFixedColumn(gridViewColumn) ||
                IsFillColumn(gridViewColumn))
            {
                e.Handled = true;
            }
        }

        private void GridColumnWidthChanged(object sender, EventArgs e)
        {
            if (!_loaded) return;

            var gridViewColumn = sender as GridViewColumn;
            if (ProportionalColumn.IsProportionalColumn(gridViewColumn) || FixedColumn.IsFixedColumn(gridViewColumn))
                return;
            
            if (RangeColumn.IsRangeColumn(gridViewColumn))
            {
                if (gridViewColumn != null && gridViewColumn.Width.Equals(double.NaN))
                {
                    _autoSizedColumn = gridViewColumn;
                    return; 
                }

                if (Math.Abs(SetRangeColumnToBounds(gridViewColumn) - 0) > ZeroWidthRange)
                    return;
                
            }

            DoResizeColumns();
        }

        private void GridColumnHeaderSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_autoSizedColumn == null) return;

            if (!(sender is GridViewColumnHeader gridViewColumnHeader) ||
                gridViewColumnHeader.Column != _autoSizedColumn) return;

            if (gridViewColumnHeader.Width.Equals(double.NaN))
            {
                gridViewColumnHeader.Column.Width = gridViewColumnHeader.ActualWidth;
                DoResizeColumns();
            }

            _autoSizedColumn = null;
        }

        private void ScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (_loaded && Math.Abs(e.ViewportWidthChange - 0) > ZeroWidthRange)
            {
                DoResizeColumns();
            }
        }

        private static void OnLayoutManagerEnabledChanged(DependencyObject dependencyObject,
                                                          DependencyPropertyChangedEventArgs e)
        {
            if (!(dependencyObject is ListView listView)) return;

            var enabled = (bool) e.NewValue;
            if (enabled)
            {
                // ReSharper disable once ObjectCreationAsStatement
                new ListViewLayoutManager(listView);
            }
        }
    }
}