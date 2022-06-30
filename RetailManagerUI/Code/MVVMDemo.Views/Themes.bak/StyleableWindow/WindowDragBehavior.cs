/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles Window dragging by Title Bar
#region ========================================================================= USING =====================================================================================
using System.Windows;
using System.Windows.Input;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public static class WindowDragBehavior
    {
        #region ========================================================== DEPENDENCY PROPERTIES ============================================================================
        public static readonly DependencyProperty LeftMouseButtonDrag = DependencyProperty.RegisterAttached("LeftMouseButtonDrag",          
            typeof(Window), typeof(WindowDragBehavior),
            new UIPropertyMetadata(null, OnLeftMouseButtonDragChanged));
        #endregion

        #region ================================================================= METHODS ===================================================================================
        public static Window GetLeftMouseButtonDrag(DependencyObject _object)
        {
            return (Window)_object.GetValue(LeftMouseButtonDrag);
        }

        public static void SetLeftMouseButtonDrag(DependencyObject _object, Window _window)
        {
            _object.SetValue(LeftMouseButtonDrag, _window);
        }

        private static void OnLeftMouseButtonDragChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement element)
                element.MouseLeftButtonDown += ButtonDown;
        }        

        private static void ButtonDown(object sender, MouseButtonEventArgs e)
        {
            UIElement element = sender as UIElement;
            if (element.GetValue(LeftMouseButtonDrag) is Window targetWindow)
                targetWindow.DragMove();
        }
        #endregion
    }
}
