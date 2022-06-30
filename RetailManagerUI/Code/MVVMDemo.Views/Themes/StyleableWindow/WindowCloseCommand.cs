/// Written by: Ciprian Horeanu
/// Creation Date: 28th of October, 2019
/// Purpose: Handles Close command of windows
#region ========================================================================= USING =====================================================================================
using System;
using System.Windows;
using System.Windows.Input;
#endregion

namespace RetailManagerUI.StyleableWindow
{
    public class WindowCloseCommand : ICommand
    {
        #region ============================================================== FIELD MEMBERS ================================================================================
        public event EventHandler CanExecuteChanged;
        #endregion

        #region ================================================================= METHODS ===================================================================================
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter is Window window)
                window.Close();
        }
        #endregion
    }
}
