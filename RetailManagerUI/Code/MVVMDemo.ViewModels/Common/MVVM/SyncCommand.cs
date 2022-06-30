
using RetailManagerUI.Models.Models;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace RetailManagerUI.ViewModels.Common.MVVM
{
    public class SyncCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Action executeSync;
        private readonly Func<bool> canExecute;
        private readonly IErrorHandler errorHandler;

        public SyncCommand(Action _execute, Func<bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeSync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }

        public bool CanExecute()
        {
            return !isExecuting && (canExecute?.Invoke() ?? true);
        }

        public void ExecuteSync()
        {
            if (CanExecute())
            {
                try
                {
                    isExecuting = true;
                    executeSync();
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit Implementation
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            try
            {
                ExecuteSync();
            }
            catch (Exception ex)
            {
                errorHandler?.HandleError(ex);
            }
        }
        #endregion
    }

    public class SyncCommand<T> : ISyncCommand<T>
    {
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Action<T> executeSync;
        private readonly Func<T, bool> canExecute;
        private readonly IErrorHandler errorHandler;

        public SyncCommand(Action<T> _execute, Func<T, bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeSync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }

        public bool CanExecute(T param)
        {
            return !isExecuting && (canExecute?.Invoke(param) ?? true);
        }       

        public void ExecuteSync(object param)
        {
            if (CanExecute((T)param))
            {
                try
                {
                    isExecuting = true;
                    executeSync((T)param);
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        public void ExecuteSync(T param)
        {
            if (CanExecute(param))
            {
                try
                {
                    isExecuting = true;
                    executeSync(param);
                }
                finally
                {
                    isExecuting = false;
                }
            }
            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit Implementation
        bool ICommand.CanExecute(object parameter)
        {
            return parameter == null || CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            try
            {
                ExecuteSync((T)parameter);
            }
            catch (Exception ex)
            {
                errorHandler?.HandleError(ex);
            }
        }
        #endregion
    }
}
