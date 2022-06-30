/// Written by: Ciprian Horeanu
/// Creation Date: 20th of October, 2019
/// Purpose: Handles Minimize command of Windows
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Input;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public class WindowMinimizeCommand : ICommand 
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
            var window = _parameter as Window;

            if (window != null)
            {
                window.WindowState = WindowState.Minimized;
            }
        }
        #endregion
    }
}
