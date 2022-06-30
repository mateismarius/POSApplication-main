/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles maximize command of Windows
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Input;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public class WindowMaximizeCommand : ICommand
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;
        #endregion

        #region ================================================================= METHODS ===================================================================================
        public bool CanExecute(object _parameter)
        {
            return true;
        }

        public void Execute(object _parameter)
        {
            if (_parameter is Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                    window.WindowState = WindowState.Normal;
                else
                    window.WindowState = WindowState.Maximized;
            }
        }
        #endregion
    }
}
