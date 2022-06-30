/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles the resizing of Windows
#region ========================================================================= USING =====================================================================================
using System.Windows;
using System.Windows.Controls.Primitives;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public static class WindowResizeBehavior
    {
        #region ========================================================== DEPENDENCY PROPERTIES ============================================================================
        public static readonly DependencyProperty TopResize = DependencyProperty.RegisterAttached("TopResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnTopResizeChanged));

        public static readonly DependencyProperty LeftResize = DependencyProperty.RegisterAttached("LeftResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnLeftResizeChanged));

        public static readonly DependencyProperty RightResize = DependencyProperty.RegisterAttached("RightResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnRightResizeChanged));

        public static readonly DependencyProperty BottomResize = DependencyProperty.RegisterAttached("BottomResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnBottomResizeChanged));

        public static readonly DependencyProperty TopLeftResize = DependencyProperty.RegisterAttached("TopLeftResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnTopLeftResizeChanged));

        public static readonly DependencyProperty TopRightResize = DependencyProperty.RegisterAttached("TopRightResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnTopRightResizeChanged));

        public static readonly DependencyProperty BottomLeftResize = DependencyProperty.RegisterAttached("BottomLeftResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnBottomLeftResizeChanged));

        public static readonly DependencyProperty BottomRightResize = DependencyProperty.RegisterAttached("BottomRightResize",
            typeof(Window), typeof(WindowResizeBehavior),
            new UIPropertyMetadata(null, OnBottomRightResizeChanged));
        #endregion

        #region ================================================================= METHODS ===================================================================================
        public static Window GetTopLeftResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(TopLeftResize);
        }

        public static void SetTopLeftResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(TopLeftResize, _window);
        }

        private static void OnTopLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragTopLeft;
        }

        public static Window GetTopRightResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(TopRightResize);
        }

        public static void SetTopRightResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(TopRightResize, _window);
        }

        private static void OnTopRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragTopRight;
        }

        public static Window GetBottomRightResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(BottomRightResize);
        }

        public static void SetBottomRightResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(BottomRightResize, _window);
        }

        private static void OnBottomRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragBottomRight;
        }

        public static Window GetBottomLeftResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(BottomLeftResize);
        }

        public static void SetBottomLeftResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(BottomLeftResize, _window);
        }

        private static void OnBottomLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragBottomLeft;
        }

        public static Window GetLeftResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(LeftResize);
        }

        public static void SetLeftResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(LeftResize, _window);
        }

        private static void OnLeftResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragLeft;
        }

        public static Window GetRightResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(RightResize);
        }

        public static void SetRightResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(RightResize, _window);
        }

        private static void OnRightResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragRight;
        }

        public static Window GetTopResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(TopResize);
        }

        public static void SetTopResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(TopResize, _window);
        }

        private static void OnTopResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragTop;
        }

        public static Window GetBottomResize(DependencyObject _object)
        {
            return (Window)_object.GetValue(BottomResize);
        }

        public static void SetBottomResize(DependencyObject _object, Window _window)
        {
            _object.SetValue(BottomResize, _window);
        }

        private static void OnBottomResizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is Thumb thumb)
                thumb.DragDelta += DragBottom;
        }

        private static void DragLeft(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(LeftResize) is Window window)
            {
                double horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);
                window.Width -= horizontalChange;
                window.Left += horizontalChange;
            }
        }

        private static void DragRight(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(RightResize) is Window window)
                window.Width += window.SafeWidthChange(e.HorizontalChange);
        }

        private static void DragTop(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(TopResize) is Window window)
            {
                double verticalChange = window.SafeHeightChange(e.VerticalChange, false);
                window.Height -= verticalChange;
                window.Top += verticalChange;
            }
        }

        private static void DragBottom(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(BottomResize) is Window window)
                window.Height += window.SafeHeightChange(e.VerticalChange);
        }

        private static void DragTopLeft(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(TopLeftResize) is Window window)
            {
                double verticalChange = window.SafeHeightChange(e.VerticalChange, false);
                double horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);
                window.Width -= horizontalChange;
                window.Left += horizontalChange;
                window.Height -= verticalChange;
                window.Top += verticalChange;
            }
        }

        private static void DragTopRight(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(TopRightResize) is Window window)
            {
                double verticalChange = window.SafeHeightChange(e.VerticalChange, false);
                double horizontalChange = window.SafeWidthChange(e.HorizontalChange);
                window.Width += horizontalChange;
                window.Height -= verticalChange;
                window.Top += verticalChange;
            }
        }

        private static void DragBottomRight(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(BottomRightResize) is Window window)
            {
                double verticalChange = window.SafeHeightChange(e.VerticalChange);
                double horizontalChange = window.SafeWidthChange(e.HorizontalChange);
                window.Width += horizontalChange;
                window.Height += verticalChange;
            }
        }

        private static void DragBottomLeft(object sender, DragDeltaEventArgs e)
        {
            Thumb thumb = sender as Thumb;
            if (thumb.GetValue(BottomLeftResize) is Window window)
            {
                double verticalChange = window.SafeHeightChange(e.VerticalChange);
                double horizontalChange = window.SafeWidthChange(e.HorizontalChange, false);
                window.Width -= horizontalChange;
                window.Left += horizontalChange;
                window.Height += verticalChange;
            }
        }

        private static double SafeWidthChange(this Window _window, double _change, bool _positive = true)
        {
            double result = _positive ? _window.Width + _change : _window.Width - _change;
            if (result <= _window.MinWidth)
                return 0;
            else if (result >= _window.MaxWidth)
                return 0;
            else if(result < 0)
                return 0;
            else
                return _change;
        }

        private static double SafeHeightChange(this Window _window, double _change, bool _positive = true)
        {
            double result = _positive ? _window.Height + _change : _window.Height - _change;
            if (result <= _window.MinHeight)
                return 0;
            else if (result >= _window.MaxHeight)
                return 0;
            else if (result < 0)
                return 0;
            else
                return _change;
        }
        #endregion
    }
}
