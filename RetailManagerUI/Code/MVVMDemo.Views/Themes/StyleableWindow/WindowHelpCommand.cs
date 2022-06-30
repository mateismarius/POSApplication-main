using RetailManagerUI.ViewModels.Common.MVVM;
using System;
using System.Windows;
using System.Windows.Input;

namespace RetailManagerUI.StyleableWindow
{
    class WindowHelpCommand : ICommand
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
                (window.DataContext as BaseModel).ShowHelp();
        }
        #endregion
    }
}
