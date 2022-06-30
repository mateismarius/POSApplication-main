/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles the behavior of the system menu shown when clicking on the icon on the Title Bars of Windows
#region ========================================================================= USING =====================================================================================
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public static class ShowSystemMenuBehavior
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        static bool leftButtonToggle = true;
        #endregion

        #region ========================================================== DEPENDENCY PROPERTIES ============================================================================
        public static readonly DependencyProperty TargetWindow = DependencyProperty.RegisterAttached("TargetWindow", typeof(Window), typeof(ShowSystemMenuBehavior));        
        
        public static readonly DependencyProperty LeftButtonShowAt = DependencyProperty.RegisterAttached("LeftButtonShowAt",
            typeof(UIElement), typeof(ShowSystemMenuBehavior),
            new UIPropertyMetadata(null, LeftButtonShowAtChanged));

        public static readonly DependencyProperty RightButtonShow = DependencyProperty.RegisterAttached("RightButtonShow",
            typeof(bool), typeof(ShowSystemMenuBehavior),
            new UIPropertyMetadata(false, RightButtonShowChanged));
        #endregion

        #region ================================================================= METHODS ===================================================================================
        #region TargetWindow
        public static Window GetTargetWindow(DependencyObject _object)
        {
            return (Window)_object.GetValue(TargetWindow);
        }

        public static void SetTargetWindow(DependencyObject _object, Window _window)
        {
            _object.SetValue(TargetWindow, _window);
        }
        #endregion

        #region LeftButtonShowAt
        public static UIElement GetLeftButtonShowAt(DependencyObject _object)
        {
            return (UIElement)_object.GetValue(LeftButtonShowAt);
        }

        public static void SetLeftButtonShowAt(DependencyObject _object, UIElement _element)
        {
            _object.SetValue(LeftButtonShowAt, _element);
        }
        #endregion

        #region RightButtonShow
        public static bool GetRightButtonShow(DependencyObject _object)
        {
            return (bool)_object.GetValue(RightButtonShow);
        }

        public static void SetRightButtonShow(DependencyObject _object, bool _argument)
        {
            _object.SetValue(RightButtonShow, _argument);
        }
        #endregion

        #region LeftButtonShowAt        
        static void LeftButtonShowAtChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement element)
                element.MouseLeftButtonDown += LeftButtonDownShow;
        }

        static void LeftButtonDownShow(object sender, MouseButtonEventArgs e)
        {
            if (leftButtonToggle)
            {
                object element = ((UIElement)sender).GetValue(LeftButtonShowAt);
                Point showMenuAt = ((Visual)element).PointToScreen(new Point(0, 0));
                Window targetWindow = ((UIElement)sender).GetValue(TargetWindow) as Window;
                SystemMenuManager.ShowMenu(targetWindow, showMenuAt);
                leftButtonToggle = !leftButtonToggle;
            }
            else
                leftButtonToggle = !leftButtonToggle;
        }
        #endregion

        #region RightButtonShow handlers
        private static void RightButtonShowChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is UIElement element)
                element.MouseRightButtonDown += RightButtonDownShow;
        }

        static void RightButtonDownShow(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            UIElement element = (UIElement)sender;
            Window targetWindow = element.GetValue(TargetWindow) as Window;
            Point showMenuAt = targetWindow.PointToScreen(Mouse.GetPosition((targetWindow)));
            SystemMenuManager.ShowMenu(targetWindow, showMenuAt);
        }
        #endregion
        #endregion
    }
}