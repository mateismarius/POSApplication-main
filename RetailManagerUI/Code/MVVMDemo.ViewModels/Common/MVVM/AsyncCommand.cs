
using RetailManagerUI.ViewModels.Common.Extensions;
using RetailManagerUI.ViewModels.Common.Interfaces;
using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RetailManagerUI.ViewModels.Common.MVVM
{
    public class AsyncCommand : IAsyncCommand
    {
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Func<Task> executeAsync;
        private readonly Func<bool> canExecute;
        private readonly IErrorHandler errorHandler;

        public AsyncCommand(Func<Task> _execute, Func<bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeAsync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }
        
        public bool CanExecute()
        {
            return !isExecuting && (canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    isExecuting = true;
                    await executeAsync();
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
        bool ICommand.CanExecute(object param)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetAsync(errorHandler);
        }
        #endregion
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        public event EventHandler CanExecuteChanged;

        private bool isExecuting;
        private readonly Func<T, Task> executeAsync;
        private readonly Func<T, bool> canExecute;
        private readonly IErrorHandler errorHandler;

        public AsyncCommand(Func<T, Task> _execute, Func<T, bool> _canExecute = null, IErrorHandler _errorHandler = null)
        {
            executeAsync = _execute;
            canExecute = _canExecute;
            errorHandler = _errorHandler;
        }

        public bool CanExecute(T param)
        {
            return !isExecuting && (canExecute?.Invoke(param) ?? true);
        }

        public async Task ExecuteAsync(T param)
        {
            if (CanExecute(param))
            {
                try
                {
                    isExecuting = true;
                    await executeAsync(param);
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
            return parameter == null || parameter.ToString() == "{DisconnectedItem}" || CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).FireAndForgetAsync(errorHandler);
        }
        #endregion
    }
}
